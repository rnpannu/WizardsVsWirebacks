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
    private List<bool> _spawnedBitmask;
    
    private float _waveTime;
    private int _currentGroup; // index
    private float _groupDelay;

    private float _groupTimeElapsed;
    private int _currentSpawnIndex;
    private float _groupSpawnInterval;

    private bool _isSpawning = false;
    public event EventHandler<SpawnEnemyEventArgs> SpawnEnemy;

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

    }

    private void SpawnWave()
    {
        // Enemy Group has [ (start time, enemy type, count, delay) ]. Start time is time after the last group,
        // delay is the interval between the enemies in this group
        // Track a cumulative start time, if wavetime has exceeded this, spawn the group 

        // Task -> Deploy this as an asynchronous operation
        // await -> Run this asynchronously but wait for the execution because I need the results, but the rest of the game can continue
        //onsole.Out.WriteLine(ToString());
        DebugLogger.Log("Initial log: " + ToString());
        Console.Out.WriteLine(_isSpawning);
        if (!_isSpawning) // If not currently spawning enemies
        {
            DebugLogger.Log("Into spawning");
            EnemyGroup cluster = _enemies[_currentGroup];
            _groupDelay += cluster.StartTime; // this is being updated too often, need to track visits
            DebugLogger.Log("Before trigger");
            if (_waveTime >= _groupDelay)
            {
                DebugLogger.Log("After trigger");
                _isSpawning = true;
                _groupSpawnInterval = (float) cluster.SpawnDelay;
                _groupTimeElapsed = 0;
                _currentSpawnIndex = 0;
                SpawnGroup(cluster);
                _currentGroup++;
                _waveTime %= _groupDelay;
            }
            _waveTime += Core.DT * 1000f;
        }
        else
        {
            _waveTime = 0;
        }
        DebugLogger.WriteLogs();

    }
    private void SpawnGroup(EnemyGroup cluster)
    {

        if (_groupTimeElapsed >= _groupSpawnInterval)
        {
            SpawnEnemy?.Invoke(this, new SpawnEnemyEventArgs(0));
            _currentSpawnIndex++;
            if (_currentSpawnIndex >= cluster.SpawnCount)
            {
                
                _isSpawning = false;
                return;
            }
            _groupTimeElapsed %= _groupSpawnInterval;
        }
        _groupTimeElapsed += Core.DT * 1000f;
    }

    public void Update()
    {
        SpawnWave();
    }

    public void Draw()
    {
        
    }

    public override string ToString()
    {
        return
            $"{nameof(_waveTime)}: {_waveTime}, {nameof(_currentGroup)}: {_currentGroup}, {nameof(_groupDelay)}: {_groupDelay}, {nameof(_groupTimeElapsed)}: {_groupTimeElapsed}, {nameof(_currentSpawnIndex)}: {_currentSpawnIndex}, {nameof(_groupSpawnInterval)}: {_groupSpawnInterval}, {nameof(_isSpawning)}: {_isSpawning}";
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