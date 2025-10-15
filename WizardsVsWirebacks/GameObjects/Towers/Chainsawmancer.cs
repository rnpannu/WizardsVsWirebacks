using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

public class Chainsawmancer : Tower
{
    public Chainsawmancer(Sprite sprite, Vector2 position) : base(sprite, position)
    {
        Sprite = sprite;
        Position = position;
        Initialize();
    }

    public override void Initialize()
    {
        
    }

    public override void LoadContent()
    {
        
    }
    
    
    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Draw(GameTime gameTime)
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }

    }
}