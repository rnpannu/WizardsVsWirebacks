using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.GameObjects.Enemies;
using WizardsVsWirebacks.GameObjects.Projectiles;

namespace WizardsVsWirebacks.Scenes;

public class LevelObjectManager
{
    private Wave _wave;
    private LevelConfig _config;
    
    private Enemy _clanka;
    private Dictionary<EnemyType, bool> _spriteUpdated = new(); // establish convention
    
    private TextureAtlas _objectAtlas;
    private AnimatedSprite _clankaSprite;
    private Sprite _chainsawSprite;

    private List<Enemy> _activeEnemies;
    private List<Tower> _activeTowers;
    private List<Projectile> _activeProjectiles;
    
    private Vector2[] _waypoints;
    private Vector2 _startPos;
    public LevelObjectManager()
    {
        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _activeEnemies = new List<Enemy>();
        _activeProjectiles = new List<Projectile>();
        _activeTowers = new List<Tower>();
        _wave = new Wave();
    }

    public void Initialize()
    {
        InitializeConfig();
        _wave.Initialize();
        _wave.SpawnEnemy += WaveOnSpawnEnemy;
        
        LoadContent();
    }
    private void InitializeConfig()
    {
        string dataPath = "";
        if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
        {
            dataPath = "../../../Content/Tilemaps/City/simplified/Level_1";
            
        } else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
        {
            dataPath = Path.Combine("Content", "Tilemaps", "City", "simplified", "Level_1");
        }
        string levelFile = Path.Combine(dataPath, "data.json");

        _config = LevelConfig.FromFile(levelFile);

        var abysmal = _config.entities.Waypoints[0];
        _startPos = new Vector2(abysmal.x, abysmal.y);
        
        var atrocious = _config.entities.Waypoints[0].customFields.Waypoint.Length;
        _waypoints = new Vector2[atrocious];
        for (int i = 0; i < atrocious; i++)
        {
            var apalling = _config.entities.Waypoints[0].customFields.Waypoint[i];
            /*_waypoints[i] = new Vector2(apalling.cx,
                apalling.cy);*/
            _waypoints[i] = new Vector2(apalling.cx * LevelConfig.LevelTileSize,
                apalling.cy * LevelConfig.LevelTileSize);
        }

    }
    public void LoadContent()
    {
        _wave.LoadContent();
    }
    
    private void WaveOnSpawnEnemy(object sender, SpawnEnemyEventArgs e)
    {
        var type = (EnemyType) e.Enemy;
        
        Enemy enemy = type switch
        { // Cube building - BuildingType
            EnemyType.Clanker => new Clanker(_objectAtlas, _waypoints, _startPos),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };

        _activeEnemies.Add(enemy);
    }
    
    public void CreateTower(int towerType, Vector2 position)
    {
        var type = (BuildingType) towerType; 
        Tower tower = type switch
        {
            BuildingType.Chainsawmancer => new Chainsawmancer(this, _objectAtlas, position),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
        if (tower is Chainsawmancer chainsawmancer)
        {
            chainsawmancer.Shoot += CreateProjectile;
        }
        _activeTowers.Add(tower);
    }
    
    
    public void CreateProjectile(Tower sourceTower, Sprite sprite, Vector2 startPosition, Vector2 startDirection)
    {
        Projectile proj = sourceTower switch // Define projectile enums?
        { 
            Chainsawmancer => new MagicBall(sourceTower, sprite, startPosition, startDirection),
            _ => throw new ArgumentException($"Unknown projectile type: {sourceTower}")
        };
        _activeProjectiles.Add(proj);
        proj.OnTimeout += DeleteProjectile;
        proj.OnCollision += HandleCollision;
    }

    private void HandleCollision(Projectile proj, Enemy enemy)
    {
        DeleteProjectile(proj);
        //enemy.Health -= 50;
    }
    
    /// <summary>
    /// Unload projectile content
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteProjectile(Projectile proj)
    {
        // This is what was done before but now it's just going to be marked so that it can be disposed without modifying an active list
        // Later add unload content logic
        //_activeProjectiles.Remove((Projectile)sender);
        proj.Dispose();
    }
    
    public void Update(GameTime gameTime)
    {
        _wave.Update();
        
        // Update core entities
        foreach (Enemy clanka in _activeEnemies)
        {
            clanka.Update(gameTime);
        }

        foreach (Tower wizard in _activeTowers)
        {
            wizard.Update(gameTime);
            if (wizard is Chainsawmancer chainsawmancer)
            {
                if (!chainsawmancer.OnCooldown)
                {
                    // TODO: Improve collision detection algorithm. Improve collision bounds checking
                    foreach (Enemy clanka in _activeEnemies)
                    {
                        if (chainsawmancer.GetRange().Intersects(clanka.GetBounds()))
                        {
                            chainsawmancer.OnTarget?.Invoke(clanka.Position);
                            break;
                        }

                    }
                }
            }
        }

        // Iterate in reverse order so modifying the list doesn't throw an exception
        for (int i = _activeProjectiles.Count - 1; i >= 0; i--)
        {
            _activeProjectiles[i].Update(gameTime);
            // Projectile timeout
            if (_activeProjectiles[i].IsDisposed)
            {
                _activeProjectiles.RemoveAt(i);
            }
        }
        
        // Projectile - Enemy collision checks
        foreach (Projectile proj in _activeProjectiles)
        {
            Tower source = proj.SourceTower;
            if (source is Chainsawmancer chainsawmancer )
            {
                // Todo: This x-y culling is pretty basic and almost redundant to the collision checks. For optimization, improve the 
                // collisison detection with strategies such as spacial partiitoning.
                float zone = chainsawmancer.Range * 1.5f;
                float left = source.Position.X - zone;
                float right = source.Position.X + zone;
                float top = source.Position.Y - zone;
                float bottom = source.Position.Y + zone;

                foreach (Enemy clanka in _activeEnemies)
                {
                    if (clanka.Position.X > left && clanka.Position.X < right)
                    {
                        if (clanka.Position.Y > top && clanka.Position.Y < bottom)
                        {
                            if (proj.GetBounds().Intersects(clanka.GetBounds()))
                            {
                                clanka.Health -= proj.Damage;
                                proj.OnCollision?.Invoke(proj, clanka);
                                Console.Out.WriteLine("Collision");
                            }
                        }
                    }
                }

            }
        }
        
    }
    
    public void Draw(GameTime gameTime)
    {
        _wave.Draw();
        foreach (Enemy clanka in _activeEnemies)
        {
            clanka.Draw(gameTime);
        }
        foreach (Tower wizard in _activeTowers)
        {
            wizard.Draw(gameTime);
        }
        foreach (Projectile proj in _activeProjectiles)
        {
            proj.Draw();
        }
    }

}