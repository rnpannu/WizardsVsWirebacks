using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.GameObjects;

public abstract class Building
{
    public int Cost { get; set; }
    public Vector2 Footprint { get; set; }// Number of tiles a building takes up
    public Rectangle Position { get; set; }
    public Sprite Sprite { get; set; }
    

    protected Building(Sprite sprite, Rectangle position)
    {
        Sprite = sprite;
        Position = position;
        Initialize();
    }
    public virtual void Initialize()
    {
        
    }

    public virtual void LoadContent()
    {

    }
    
    public virtual void HandleInput()
    {
        
    }
    
    public virtual void Update()
    {
        
    }

    public virtual void Draw()
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }

    }
    
}