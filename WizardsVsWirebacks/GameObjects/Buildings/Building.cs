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

    
    private const float _printDelay = 2000f;
    private float _pdCounter = 0;

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

            if (_pdCounter > _printDelay)
            {
                //Console.Out.WriteLine("Rectangle: " + Position.ToString());
                // * Create debug interface, massive switch for controlling console output?
                // * the non vim mfs could just use a debugger but we have to consider their kind
                //Console.Out.WriteLine(CityConfig.Width.ToString() + CityConfig.Height.ToString());
                _pdCounter %= _printDelay;
            }
            else
            {
                _pdCounter += Core.DT * 1000f;
            }
            Sprite.Draw(Core.SpriteBatch, Position);
        }

    }
    
}