using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

public class ChainsawmancerBuilding : Building
{
    
    public ChainsawmancerBuilding(Sprite sprite, Rectangle position) : base(sprite, position)
    {
        Footprint = new Vector2(4, 5);
        Position = new Rectangle(position.X, position.Y, position.Width * (int)Footprint.X, position.Height * (int) Footprint.Y);
    }

    public void Initialize()
    {
        LoadContent();
    }

    public void LoadContent()
    {
        
    }
    

}