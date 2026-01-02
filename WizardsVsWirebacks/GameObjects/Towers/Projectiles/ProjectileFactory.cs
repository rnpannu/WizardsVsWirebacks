using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects.Projectiles;

public class ProjectileFactory
{
    private readonly TextureAtlas _atlas;
    
    public ProjectileFactory(TextureAtlas atlas)
    {
        _atlas = atlas;
    }
    
    public Projectile CreateMagicBall(Vector2 position, Vector2 direction)
    {
        Sprite sprite = _atlas.CreateSprite("sword-1");
        
        return new MagicBall(sprite, position, direction);
    }
}