using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.Scenes;

public class Level
{
    private LevelConfig _config;

    private Vector2 _startPos;

    private Vector2[] _waypoints;

    private List<Enemy> _wave;

    private TextureAtlas _objectAtlas;
    private Sprite _clankaSprite;
    public Level()
    {
        Initialize();
    }

    /// <summary>
    /// Load config from JSON file
    /// </summary>
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
            _waypoints[i] = new Vector2(apalling.cx,
                apalling.cy);
        }
        
        /*Console.Out.WriteLine(_startPos.ToString());
        foreach (var wayp in _waypoints)
        {
            Console.Out.WriteLine(wayp.ToString());
        }*/
    }
    public void Initialize()
    {
        InitializeConfig();
        LoadContent();
    }

    private void LoadContent()
    {
        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        //_clankaSprite = _objectAtlas.CreateSprite("clanka-1");
    }
    public void CreateEnemy(int id, Rectangle position)
    {
        var type = (EnemyType) id;
        Console.Out.WriteLine(position.ToString());
        Enemy enemy = type switch
        { // Cube building - BuildingType
            EnemyType.Clanker => new Clanker(_clankaSprite, _waypoints, _startPos),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };

        _wave.Add(enemy);
    }
}