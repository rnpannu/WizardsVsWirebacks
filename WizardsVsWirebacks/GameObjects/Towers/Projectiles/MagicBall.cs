using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects.Projectiles;

public class MagicBall : Projectile
{
    public MagicBall(Sprite sprite, Vector2 startPosition, Vector2 startDirection) : base(sprite, startPosition, startDirection)
    {
        Sprite = sprite;
        _movementSpeed = 50; //px per second? who knows! 
        _lifespan = TimeSpan.FromMilliseconds(5000);
    }
    
} 