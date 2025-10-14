using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.Scenes;

public class LevelObjectManager
{
    private Wave _wave;
    private LevelConfig _config;
    private Enemy _clanka;
    private TextureAtlas _objectAtlas;
    private Sprite _clankaSprite;

    private List<Enemy> _activeEnemies;
    
    private Vector2[] _waypoints;
    private Vector2 _startPos;
    public LevelObjectManager()
    {
        _activeEnemies = new List<Enemy>();
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
        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _clankaSprite = _objectAtlas.CreateSprite("clanka-1");
        _clankaSprite.Origin = new Vector2(_clankaSprite.Width * 0.25f, _clankaSprite.Height * 0.25f); // Centers it in a 16px tile!
        _wave.LoadContent();
    }
    
    private void WaveOnSpawnEnemy(object sender, SpawnEnemyEventArgs e)
    {
        var type = (EnemyType) e.Enemy;
        Enemy enemy = type switch
        { // Cube building - BuildingType
            EnemyType.Clanker => new Clanker(_clankaSprite, _waypoints, _startPos),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
        _activeEnemies.Add(enemy);
    }

    public void Update()
    {
        _wave.Update();
        foreach (Enemy clanka in _activeEnemies)
        {
            clanka.Update();
        }
        /*if (_clanka != null)
        {
            _clanka.Update();
        }
        else
        {
            CreateEnemy(0);
        }*/


    }

    public void Draw()
    {
        _wave.Draw();
        foreach (Enemy clanka in _activeEnemies)
        {
            clanka.Draw();
        }

    }

}