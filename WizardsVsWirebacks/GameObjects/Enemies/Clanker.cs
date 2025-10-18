using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public class Clanker : Enemy
{
    public Clanker(TextureAtlas atlas, Vector2[] waypoints, Vector2 position) : base(atlas, waypoints, position)
    {
        _movementSpeed = 100;
        // ! Refactor. Every enemy probably does not need its own copy of waypoints 
        // ! Create a object/wave manager that manages the enemies

    }

    public override void Initialize(TextureAtlas atlas)
    {
        base.Initialize(atlas);
    }

    public override void LoadContent(TextureAtlas objectAtlas)
    {
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-up"));
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-down"));
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-right"));
        _sprite = objectAtlas.CreateAnimatedSprite("clanka-walk-down");
        base.LoadContent(objectAtlas);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    



}