using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

public abstract class Tower
{
    public int Cost { get; set; }
    public Vector2 Position { get; protected set; }
    public Sprite Sprite { get; set; }
    
    public int Range { get; protected set; }

    protected Tower(Sprite sprite, Vector2 position)
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

    public Circle GetBounds()
    {
        
        Circle bounds = new Circle(
            (int)(Position.X + (Sprite.Width * 0.5f)),
            (int)(Position.Y + (Sprite.Height * 0.5f)),
            (int)(Sprite.Width * 0.5f)
        );

        return bounds;
    }
    
}
