using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public class Clanker : Enemy
{
    public Clanker(TextureAtlas atlas, Vector2[] waypoints, Vector2 position) : base(atlas, waypoints, position)
    {
        _movementSpeed = 60;
    }

    public override void Initialize(TextureAtlas atlas)
    {
        base.Initialize(atlas);
    }
    /// TODO: Review. I think this creates a new sprite for every object of this type. Obviously right now
    /// I think that's fine but in the future I think aiming for the flyweight pattern
    /// (sharing a single sprite instance) would be a good performance goal. I was talking to Marcus and
    /// Rohan and that was the way I implemented it before, but I prototyped this as a way to encapsulate asset
    /// loading to its own relevant object, but it removed that resource sharing element. I want people to brainstorm
    /// how to best balance function, flexibility, and neatness in the creating of objects and loading of assets.
    /// Some thoughts:
    ///     - Consolidated asset manager that loads and inject assets
    ///     - Hybrid approach of above and current. Each object creates its own assets but
    ///       registers them to a central manager and checks if one exists
    ///     - Full factory / abstract factory implementation with this flyweight sharing integrated in
    public override void LoadContent(TextureAtlas objectAtlas)
    {
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-up"));
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-down"));
        _animations.Add(objectAtlas.GetAnimation("clanka-walk-right"));
        _sprite = objectAtlas.CreateAnimatedSprite("clanka-walk-down");
        
        // ? Theoretical psuedocode
        /// if (objectAtlas.Sprites.Exists("clankaSprite")){
        ///     _sprite = objectAtlas
        base.LoadContent(objectAtlas);
    }

    public override Circle GetBounds()
    {
        Circle bounds = new Circle(
            (int)(Position.X + (_sprite.Width * 0.5f)),
            (int)(Position.Y + (_sprite.Height * 0.5f)),
            (int)(_sprite.Width * 0.25f) // 0.25f currently for skeleton with whitespace?
        );
        return bounds;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    



}