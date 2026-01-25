using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.ExtensionMethods;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public abstract class Enemy
{
    // protected const int MOVEMENT_BUFFER = 15;
    protected int Health { get; set; }
    
    // Core
    protected AnimatedSprite _sprite;
    protected List<Animation> _animations;
    
    // Traversal
    private Vector2 _position;
    protected Vector2 _nextPosition;
    
    protected Vector2[] _waypoints;
    protected int _currentWayPoint = 0;
    
    protected int _movementSpeed;
    protected bool _switchDir = false;
    public Vector2 Dir { get; protected set; }
    
    public Vector2 Position
    {
        get => _position;
        protected set => _position = value;
    }

    
    public Enemy(TextureAtlas atlas, Vector2[] waypoints, Vector2 position)
    {
        _waypoints = waypoints; // Apparently this is by reference instead of copy. Arrays are on the heap i guess
        _currentWayPoint = 0;
        Position = position;
        _nextPosition = waypoints[_currentWayPoint];
        Dir = Vector2.Normalize(_nextPosition - Position);

        _animations = new List<Animation>();
        Initialize(atlas);
    }

    public virtual void Initialize(TextureAtlas atlas)
    {
        LoadContent(atlas);
        _sprite.Origin = new Vector2(_sprite.Width * 0.25f, _sprite.Height * 0.25f); // Centers it in a 16px tile!
    }
    public virtual void LoadContent(TextureAtlas atlas)
    {
        
    }

    private void UpdateSprite(GameTime gameTime)
    {
        if (_switchDir)
        {
            _sprite.Effects = SpriteEffects.None;
            if (Math.Abs(Dir.Y) > (Math.Abs(Dir.X) * 1.5)) // Prefer horizontal animations unless incline is near perpendicular
            {
                if (Dir.Y < 0)
                {
                    _sprite.Animation = _animations[0];
                }
                else
                {
                    _sprite.Animation = _animations[1];
                    
                }
            }
            else
            {
                if (Dir.X < 0)
                {
                    _sprite.Animation = _animations[2];
                    
                    // Currently, clanker has no unique left/right walks, so we invert. May need to change this behaviour for other assets
                    _sprite.Effects = SpriteEffects.FlipHorizontally;
                }
                
                else
                {
                    _sprite.Animation = _animations[2];
                } 
            }

            _switchDir = false;
        }

        _sprite.Update(gameTime);
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += ((Dir * Core.DT * _movementSpeed));
         if (Vector2.DistanceSquared(Position, _nextPosition) < 2) // magic number
         {
             _switchDir = true;
            _nextPosition = _waypoints[_currentWayPoint + 1];
            Dir = Vector2.Normalize(_nextPosition - _waypoints[_currentWayPoint]);
            if (_currentWayPoint < _waypoints.Length)
            {
                _currentWayPoint++;
            }
            else
            {
                this.Despawn();
            }
        }
        UpdateSprite(gameTime);
    }

    private void Despawn()
    {
        throw new NotImplementedException();
    }

    public virtual void Draw(GameTime gameTime)
    {
        _sprite.Draw(Core.SpriteBatch, Position);
    }
    public virtual Circle GetBounds()
    {
        Circle bounds = new Circle(
            (int)(Position.X + (_sprite.Width * 0.5f)),
            (int)(Position.Y + (_sprite.Height * 0.5f)),
            (int)(_sprite.Width * 0.25f) // 0.25f currently for skeleton with whitespace?
        );

        return bounds;
    }
}