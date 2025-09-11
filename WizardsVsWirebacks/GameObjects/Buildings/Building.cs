using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

// TODO: Learn wtf interfaces do again
//       Configure a better blueprint for all buildings
public interface Building
{
    int Cost { get; set; }

    // Number of tiles a building takes up
    int Footprint { get; protected set; }

    Vector2 GridPosition { get; set; }

    Sprite Sprite { protected get; set; }
    
    //~ Decide what animation(s) we want for buildings
    // public AnimatedSprite _animatedSprite { protected get; set; }

    
    public void Initialize()
    {
        
    }

    public void LoadContent()
    {
        
    }
    
    public void OnPlace()
    {
        
    }

    public void OnRemove()
    {
        
    }

    public void HandleInput()
    {
        
    }
    
    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw()
    {
    }
}