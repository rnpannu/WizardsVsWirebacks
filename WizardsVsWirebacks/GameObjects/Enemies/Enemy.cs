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
    protected AnimatedSprite _currentSprite;
    protected List<AnimatedSprite> _sprites;
    protected int _movementSpeed;
    protected Vector2[] _waypoints;
    protected int _currentWayPoint = 0;
    protected Vector2 _currentPosition;
    protected Vector2 _nextPosition;
    protected Vector2 Dir { get; set; }
    

    protected int Health { get; set; }
    

    public Enemy(AnimatedSprite currentSprite, Vector2[] waypoints, Vector2 position)
    {
        _currentSprite = currentSprite;
        _waypoints = waypoints; // Apparently this is by reference instead of copy. Arrays are on the heap i guess
        _currentWayPoint = 0;
        _currentPosition = position;
        _nextPosition = waypoints[_currentWayPoint];
        Dir = Vector2.Normalize(_nextPosition - _currentPosition);
        Initialize();
        
    }

    public virtual void Initialize()
    {
        
    }
    public virtual void LoadContent()
    {
        
    }

    private void UpdateSprite(GameTime gameTime)
    {
        if (Math.Abs(Dir.Y) > Math.Abs(Dir.X))
        {
            if (Dir.Y < 0)
            {
                _currentSprite = _sprites[0];
            }
            else
            {
                _currentSprite = _sprites[1];
            }
        }
        else
        {
            if (Dir.X < 0)
            {
                _currentSprite = _sprites[2];
            }
            else
            {
                _currentSprite = _sprites[3];
            }
        }

    }
    public virtual void Update(GameTime gameTime)
    {
        
        //_sprite.Update(gameTime);
        _currentPosition += ((Dir * Core.DT * _movementSpeed));
        
         if (Vector2.DistanceSquared(_currentPosition, _nextPosition) < 5)
        {
            _nextPosition = _waypoints[_currentWayPoint + 1];
            Dir = Vector2.Normalize(_nextPosition - _waypoints[_currentWayPoint]);
            if (_currentWayPoint < _waypoints.Length)
            {
                _currentWayPoint++;
            }
        }
    }

    public virtual void Draw(GameTime gameTime)
    {
        _currentSprite.Draw(Core.SpriteBatch, _currentPosition);
    }
    
}