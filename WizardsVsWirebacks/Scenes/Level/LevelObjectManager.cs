using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.GameObjects.Enemies;

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
    
    private Vector2[] _waypoints;
    private Vector2 _startPos;
    public LevelObjectManager()
    {
        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _activeEnemies = new List<Enemy>();
        _activeTowers = new List<Tower>();
        _wave = new Wave();
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
    public void Initialize()
    {
        InitializeConfig();
        _wave.Initialize();
        _wave.SpawnEnemy += WaveOnSpawnEnemy;
        LoadContent();
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
        var type = (BuildingType) towerType; // Placeholder
        Tower tower = type switch
        { // Cube building - BuildingType
            BuildingType.Chainsawmancer => new Chainsawmancer(_objectAtlas, position),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
        _activeTowers.Add(tower);
    }

    public void Update(GameTime gameTime)
    {
        _wave.Update();
        foreach (Enemy clanka in _activeEnemies)
        {
            clanka.Update(gameTime);
        }
        
        // Maybe projectiles should be a struct to be stack allocated and more performant
        foreach (Tower wizard in _activeTowers)
        {
            wizard.Update(gameTime, _activeEnemies);
            
            // Dead code, leaving for reference
            // O(n^2), will break loop once targetting is found? - BAD, replace with better system
            /*foreach (Enemy clanka in _activeEnemies)
            {
                // Scuffed asf, move to tower-specific update? - needs enemy information
                if (wizard.GetType() == typeof(ProjectileTower))
                {
                    var projectileTower = (ProjectileTower)wizard;
                    if (projectileTower.GetRange().Intersects(clanka.GetBounds()))
                    {
                        projectileTower.Target(clanka.GetBounds());
                        break;
                        
                    }
                }

            }*/
            
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


    }

}