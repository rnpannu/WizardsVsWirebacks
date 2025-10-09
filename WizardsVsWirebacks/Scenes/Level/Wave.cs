using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.Scenes;

public class Wave
{
    private List<EnemyGroup> _enemies;
    private Enemy _clanka;
    private TextureAtlas _objectAtlas;
    private Sprite _clankaSprite;

    private int _spawnedGroup;
    private float _waveTime;
    // private EventHandler<___> livesLost;
    private void InitializeConfig()
    {
        string dataPath = "";
        if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
        {
            dataPath = "../../../Content/Waves";
            
        } else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
        {
            dataPath = Path.Combine("Content", "Waves");
        }
        string levelFile = Path.Combine(dataPath, "wave1-definition.json");
        _enemies = EnemyGroup.FromFile(levelFile);
        // [ (start time, enemy type, count, delay) ]
    }
    public void Initialize()
    {
        _enemies = new List<EnemyGroup>();
        InitializeConfig();
    }
    public void LoadContent()
    {
        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _clankaSprite = _objectAtlas.CreateSprite("clanka-1");
        _clankaSprite.Origin = new Vector2(_clankaSprite.Width * 0.25f, _clankaSprite.Height * 0.25f); // Centers it in a 16px tile!
    }

    private async Task SpawnWave()
    {
        // [ (start time, enemy type, count, delay) ]
        // Track a cumulative start time, if wavetime has exceeded this, spawn the group with their delay in spacing
        float spawnTime = 0;
        // Task => Deploy this as an asynchronous operation
        // await => Run this asynchronously but wait for the execution because I need the results, but the rest of the game can continue
        foreach (EnemyGroup cluster in _enemies)
        {
            spawnTime += cluster.StartTime;
            if (_waveTime >= spawnTime)
            {
                await Task.Run(() => SpawnGroup(cluster));
            }
        }
        _waveTime += Core.DT * 1000f;
    }
    private void SpawnGroup(EnemyGroup cluster)
    {
        
        for (int i = 0; i < cluster.SpawnCount; i++)
        {
            CreateEnemy(cluster.EnemyType);
        }
    }
    public void CreateEnemy(int id, Rectangle position)
    {
        var type = (EnemyType) id;
        Enemy enemy = type switch
        { // Cube building - BuildingType
            EnemyType.Clanker => new Clanker(_clankaSprite, _waypoints, _startPos),
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
    }
    public void Update()
    {
        
    }

    public void Draw()
    {
        
    }
}