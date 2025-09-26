using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.GameObjects;

public abstract class Building
{
    public BuildingType Type { get; protected set; }
    public int Cost { get; set; }
    public int Footprint { get; protected set; }// Number of tiles a building takes up
    public Vector2 GridPosition { get; set; }
    public Sprite Sprite { get; set; }


    protected Building(BuildingType type, int cost, int footprint, Sprite sprite)
    {
        Type = type;
        Cost = cost;
        Footprint = footprint;
        Sprite = sprite;
    }
    //~ Decide what animation(s) we want for buildings
    // public AnimatedSprite _animatedSprite { protected get; set; }
    public void OnPlace()
    {
        CityScene.Doubloons -= Cost;
    }
    
    public void Initialize()
    {
        
    }

    public void LoadContent()
    {
        
    }


    public void OnRemove()
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
            //Sprite.Draw(); 
        }

    }
    
}