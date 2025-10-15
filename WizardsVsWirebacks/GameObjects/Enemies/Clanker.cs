using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public class Clanker : Enemy
{
    public Clanker(AnimatedSprite currentSprite, Vector2[] waypoints, Vector2 position) : base(currentSprite, waypoints, position)
    {
        _movementSpeed = 100;
        // ! Refactor. Every enemy probably does not need its own copy of waypoints 
        // ! Create a object/wave manager that manages the enemies

    }

    private void LoadContent(TextureAtlas objectAtlas)
    {
        _sprites.Add(objectAtlas.CreateAnimatedSprite("clanka-walk-up"));
        _sprites.Add(objectAtlas.CreateAnimatedSprite("clanka-walk-down"));
        _sprites.Add(objectAtlas.CreateAnimatedSprite("clanka-walk-left"));
        _sprites.Add(objectAtlas.CreateAnimatedSprite("clanka-walk-right"));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    



}