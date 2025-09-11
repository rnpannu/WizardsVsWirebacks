using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

public class Tower1Building : Building
{
    
    public int Cost { get; set; } = 100;

    public int Footprint { get; set; } = 1;
    
    public Vector2 GridPosition { get; set; } 
    
    public Sprite Sprite { get; set; }
    
    // TODO: Figure all this shit out
    public Tower1Building(Sprite sprite)
    {
        Sprite = sprite;
        //Initialize();
    }

    public void Initialize(Vector2 tile)
    {
        GridPosition = tile;
    }
    
   


}