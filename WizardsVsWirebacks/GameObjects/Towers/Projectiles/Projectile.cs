using  System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects.Projectiles;

public abstract class Projectile : IDisposable
{
    public Sprite Sprite;
    protected float _movementSpeed;
    protected Vector2 _position;
    protected Vector2 _dir;
    
    protected TimeSpan _timeAlive;
    protected TimeSpan _lifespan;

    public bool IsDisposed { get; private set;  }

    public Projectile(Sprite sprite, Vector2 startPosition, Vector2 direction)
    {
        Sprite = sprite;
        _position = startPosition;
        _dir = direction;
    }
    ~Projectile() => Dispose(false);

    public virtual void LoadContent()
    {
        
    }
    public virtual void UnloadContent()
    {
        //Core.Content.UnloadAsset(assetName); 
    }
    public virtual void Update(GameTime gameTime)
    {
        if(_dir != Vector2.Zero)
        {
            _dir.Normalize();
        }
        _position += _dir * _movementSpeed * Core.DT;
        _timeAlive += TimeSpan.FromMilliseconds(Core.DT * 1000);
        if (_timeAlive >= _lifespan) // Replace with OnTimeout event
        {
            Dispose();
        } 
    }

    public virtual void Draw()
    {
        Sprite.Draw(Core.SpriteBatch, _position);
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
            //Core.Content.Dispose();
        }
    }
    // Getbounds is used a lot throughout our code - encapsulate?
    public Circle GetBounds()
    {
        return Circle.Empty;
    }


}