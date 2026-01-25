using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.GameObjects;

public abstract class Tower
{
    public int Cost { get; set; }
    public Vector2 Position { get; set; }
    public Sprite Sprite { get; set; }
    
    protected Tower(TextureAtlas atlas, Vector2 position)
    {
        //TODO: Establish a null sprite?
        //Sprite = atlas.CreateSprite("null-sprite");
        Position = position;
        Initialize();
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void LoadContent()
    {
        
    }
    
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(GameTime gameTime)
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }

    }

    public virtual Circle GetBounds()
    {
        
        Circle bounds = new Circle(
            (int)(Position.X +  + (Sprite.Width * 0.5f)),
            (int)(Position.Y + (Sprite.Height * 0.5f)),
            (int)(Sprite.Width * 0.5f)
        );

        return bounds;
    }

}