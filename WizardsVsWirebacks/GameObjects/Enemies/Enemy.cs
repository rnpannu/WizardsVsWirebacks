using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.ExtensionMethods;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.UI;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public abstract class Enemy
{
    // protected const int MOVEMENT_BUFFER = 15;
    protected int _health;
    protected int _maxHealth;
    protected Healthbar _healthbar;
    
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

    public Action SwitchDir;
    public Action<int> HealthChanged;
    
    public Enemy(TextureAtlas atlas, Vector2[] waypoints, Vector2 position)
    {
        _waypoints = waypoints; // Apparently this is by reference instead of copy. Arrays are on the heap i guess
        _currentWayPoint = 0;
        Position = position;
        _nextPosition = waypoints[_currentWayPoint];
        Dir = Vector2.Normalize(_nextPosition - Position);
        
        _animations = new List<Animation>();

        SwitchDir += OnSwitchDir;
        Initialize(atlas);
    }
    public Vector2 Position
    {
        get => _position;
        protected set
        {
            if (Vector2.DistanceSquared(Position, _nextPosition) < 2) // magic number
            {
                SwitchDir?.Invoke();
            }
            _position = value;
        }
    }
    public Vector2 Dir { get; protected set; }
    // Basic getter so going in the properties section
    public virtual Circle GetBounds()
    {
        Circle bounds = new Circle(
            (int)(Position.X + (_sprite.Width * 0.5f)),
            (int)(Position.Y + (_sprite.Height * 0.5f)),
            (int)(_sprite.Width * 0.25f) // 0.25f currently for skeleton with whitespace?
        );

        return bounds;
    }

    public int Health
    {
        get => _health;
        set
        {
            if (value > 0 && value <= _maxHealth)
            {
                _health += value;
                _healthbar.HealthChanged?.Invoke(_health);
            }
            else if (value <= 0)
            {
                Despawn();
            }
        }
    }

    public virtual void Initialize(TextureAtlas atlas)
    {
        LoadContent(atlas);
    }
    public virtual void LoadContent(TextureAtlas atlas)
    {
        _sprite.Origin = new Vector2(_sprite.Width * 0.25f, _sprite.Height * 0.25f); // Centers it in a 16px tile!
        _healthbar = new Healthbar(new Vector2(Position.X, Position.Y - _sprite.Origin.Y), Health);
    }

    private void OnSwitchDir()
    {
        _nextPosition = _waypoints[_currentWayPoint + 1];
        Dir = Vector2.Normalize(_nextPosition - _waypoints[_currentWayPoint]);
        if (_currentWayPoint < _waypoints.Length)
        {
            _currentWayPoint++;
            ChangeAnimation();
        }
        else
        {
            this.Despawn();
        }
    }
    protected virtual void ChangeAnimation()
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
                // Making method private for this reason
                _sprite.Effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                _sprite.Animation = _animations[2];
            } 
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += ((Dir * Core.DT * _movementSpeed));
        _sprite.Update(gameTime);
        _healthbar.Update(gameTime, new Vector2(Position.X + (_sprite.Origin.Y * 0.475f), Position.Y - (_sprite.Origin.Y * 0.6f)));
    }

    private void Despawn()
    {
        throw new NotImplementedException();
    }

    public virtual void Draw(GameTime gameTime)
    {
        _sprite.Draw(Core.SpriteBatch, Position);
        _healthbar.Draw(gameTime);
    }

}