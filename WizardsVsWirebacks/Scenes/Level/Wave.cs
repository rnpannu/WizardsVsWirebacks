using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private List<bool> _visited;
    private List<bool> _spawned;
    
    private float _waveTimeElapsed;
    private int _currentGroupIndex; // index
    private float _groupSpawnDelay;

    private float _groupTimeElapsed;
    private int _currentSpawnIndex;
    private float _groupSpawnInterval;

    private bool _spawning = false;
    public event EventHandler<SpawnEnemyEventArgs> SpawnEnemy;

    // private EventHandler<___> livesLost;
    private void InitializeConfig()
    {
        string dataPath = "";
        if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
        {
            dataPath = "../../../Content/Waves";

        }
        else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
        {
            dataPath = Path.Combine("Content", "Waves");
        }

        string levelFile = Path.Combine(dataPath, "wave1-definition.json");
        _enemies = EnemyGroup.FromFile(levelFile);
        for (int i = 0; i < _enemies.Count; i++)
        {
            _visited.Add(false);
            _spawned.Add(false);

        }
    // [ (start time, enemy type, count, delay) ]
    }
    public void Initialize()
    {
        _enemies = new List<EnemyGroup>();
        _visited = new List<bool>();
        _spawned = new List<bool>();
        InitializeConfig();
    }
    public void LoadContent()
    {
        
    }

    private void SpawnWave()
    {
        
        EnemyGroup cluster = _enemies[_currentGroupIndex];
        if (!_spawning && (_currentGroupIndex < _enemies.Count))
        {
            if (_waveTimeElapsed >= cluster.StartTime)
            {
                _spawning = true;
                _groupSpawnInterval = (float) cluster.SpawnDelay;
                _groupTimeElapsed = 0;
                _currentSpawnIndex = 0;
                _waveTimeElapsed %= _groupSpawnDelay;
            }
            else
            {
                _waveTimeElapsed += Core.DT;
            }
        }
        else
        {
            SpawnGroup(cluster);
            _waveTimeElapsed = 0;
        }

    }
    private void SpawnGroup(EnemyGroup cluster)
    {

        if (_groupTimeElapsed >= _groupSpawnInterval)
        {
            SpawnEnemy?.Invoke(this, new SpawnEnemyEventArgs(0));
            _currentSpawnIndex++;
            if (_currentSpawnIndex >= cluster.SpawnCount)
            {
                _groupSpawnInterval = 0;
                _groupTimeElapsed = 0;
                _currentSpawnIndex = 0;                   
                if (_currentGroupIndex < _enemies.Count - 1)
                {
                    _currentGroupIndex++;
                }      
                _spawning = false;
                return;
            }
            _groupTimeElapsed %= _groupSpawnInterval;
        }
        else
        {
            _groupTimeElapsed += Core.DT ;
        }

    }

    public void Update()
    {
        SpawnWave();
    }

    public void Draw()
    {
        
    }
    
}

public class SpawnEnemyEventArgs : EventArgs
{
    public int Enemy { get; }
    
    public SpawnEnemyEventArgs(int enemy)
    {
        Enemy = enemy;
    }
}