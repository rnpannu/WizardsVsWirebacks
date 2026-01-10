using  System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects.Projectiles;

public abstract class Projectile : IDisposable
{




    public Tower SourceTower { get; protected set; }
    public Sprite Sprite { get; protected set; }
    public Vector2 Position { get; protected set; }
    public float MovementSpeed { get; protected set; }
    public Vector2 Dir { get; protected set; }
    
    public TimeSpan LifeSpan { get; protected set; }

    protected TimeSpan _timeAlive;
    
    public event EventHandler OnTimeout;
    public event EventHandler OnCollision;
    public bool IsDisposed { get; private set;  }

    public Projectile(Tower sourceTower, Sprite sprite, Vector2 startPosition, Vector2 direction)
    {
        SourceTower = sourceTower;
        Sprite = sprite;
        Position = startPosition;
        Dir = direction;
    }
    ~Projectile() => Dispose(false);

    public virtual void LoadContent()
    {
        
    }
    public virtual void UnloadContent()
    {
        
    }
    public virtual void Update(GameTime gameTime)
    {
        if(Dir != Vector2.Zero)
        {
            Dir.Normalize();
        }
        Position += Dir * MovementSpeed * Core.DT;
        _timeAlive += TimeSpan.FromMilliseconds(Core.DT * 1000);
        if (_timeAlive >= LifeSpan) 
        {
            OnTimeout?.Invoke(this, EventArgs.Empty);
            Dispose();
        } 
    }

    public virtual void Draw()
    {
        Sprite.Draw(Core.SpriteBatch, Position);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            UnloadContent();
        }

        IsDisposed = true;
    }
    // Getbounds is used a lot throughout our code - encapsulate?
    public Circle GetBounds()
    {
        Circle bounds = new Circle(
            (int)(Position.X + (Sprite.Width * 0.5f)),
            (int)(Position.Y + (Sprite.Height * 0.5f)),
            (int)(Sprite.Width * 0.25f) // 0.25f currently for skeleton with whitespace?
        );

        return bounds;
    }


}