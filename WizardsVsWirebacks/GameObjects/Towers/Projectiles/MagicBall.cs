using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects.Projectiles;

public class MagicBall : Projectile
{
    public MagicBall(Tower sourceTower, Sprite sprite, Vector2 startPosition, Vector2 startDirection) : base(sourceTower, sprite, startPosition, startDirection)
    {
        Damage = 50;
        MovementSpeed = 100; //px per second? who knows! 
        LifeSpan = TimeSpan.FromMilliseconds(2000);
    }
    
} 