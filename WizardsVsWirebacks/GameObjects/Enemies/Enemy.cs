using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGameGum.ExtensionMethods;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public abstract class Enemy
{
    protected const int MOVEMENT_BUFFER = 15;
    protected Sprite _sprite;
    protected int _movementSpeed;
    protected Vector2[] _waypoints;
    protected int _currentWayPoint = 0;
    protected Vector2 _currentPosition;
    protected Vector2 _nextPosition;
    protected Vector2 Dir => Vector2.Normalize(_nextPosition - _currentPosition);
    



    protected int Health { get; set; }
    

    public Enemy(Sprite sprite, Vector2[] waypoints, Vector2 position)
    {
        _sprite = sprite;
        _waypoints = waypoints; // Apparently this is by reference instead of copy. Arrays are on the heap i guess
        _currentWayPoint = 0;
        _currentPosition = position;
        _nextPosition = waypoints[_currentWayPoint];
        Console.Out.WriteLine("Create enemy object with starting pos: " + _currentPosition.ToString());
        foreach (var waypoint in waypoints)
        {
            Console.Out.WriteLine("Waypoint: " + waypoint.ToString());
        }
        Console.Out.WriteLine("Direction to next waypoint: " + Dir.ToString() + "\n\n\n");
        Initialize();
    }

    public virtual void Initialize()
    {
        
    }
    public virtual void LoadContent()
    {
        
    }
    public virtual void Update()
    {
        DebugLogger.Log(this.ToString());
        DebugLogger.WriteLogs();
        _currentPosition += ((Dir * Core.DT * _movementSpeed));

        // ? Improve with Vector2.DistanceSquared?
        // N
        if ((_currentPosition.X >= _nextPosition.X - 10 && _currentPosition.X <= _nextPosition.X + 10) && 
            (_currentPosition.Y >= _nextPosition.Y - 10 && _currentPosition.Y <= _nextPosition.Y + 10))
        {
            if (_currentWayPoint < _waypoints.Length)
            {
                _currentWayPoint++;
            }
            _nextPosition = _waypoints[_currentWayPoint];
        }
    }

    public virtual void Draw()
    {
        _sprite.Draw(Core.SpriteBatch, _currentPosition);
    }

    public override string ToString()
    {
        return
            $"{nameof(_sprite)}: {_sprite}, {nameof(_movementSpeed)}: {_movementSpeed}, {nameof(_waypoints)}: {_waypoints}, {nameof(_currentWayPoint)}: {_currentWayPoint}, {nameof(_currentPosition)}: {_currentPosition}, {nameof(_nextPosition)}: {_nextPosition}, {nameof(Dir)}: {Dir}, {nameof(Health)}: {Health}";
    }
}