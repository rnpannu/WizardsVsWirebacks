using  System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;

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
    
    // the event keyword means that only that object can invoke its events, a regular delegate can be invoked by anyone with a reference (i think)
    // events can also only be subscribed or unsubscribed += -= to outside its containing class
    public event Action<Projectile> OnTimeout;
    public Action<Projectile, Enemy> OnCollision;
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
            OnTimeout?.Invoke(this); 
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