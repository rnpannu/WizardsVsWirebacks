using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.GameObjects;

public abstract class Building
{
    public BuildingType Type { get; protected set; }
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
    public void Initialize()
    {

    }

    public void LoadContent()
    {

    }
    
    public void HandleInput()
    {
        
    }
    
    public void Update()
    {
        
    }

    public void Draw()
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }

    }
    
}