using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;
using WizardsVsWirebacks.GameObjects.Projectiles;
using WizardsVsWirebacks.Scenes;

namespace WizardsVsWirebacks.GameObjects;

public class Chainsawmancer : Tower
{

    // Core
    private Sprite _projectileSprite;
    
    // Targeting
    protected Vector2 _currentTarget;
    protected float _shootCooldown;
    protected float _shootTimer;
    
    protected float _snapTime;
    protected float _startingAngle;
    protected float _destinationAngle;
    public int Range { get; protected set; }
    public bool OnCooldown { get; protected set; }
    
    public Action<Vector2> OnTarget;
    public Action<Tower, Sprite, Vector2, Vector2> Shoot; // event keyword enforces that this is the only object that can invoke or null this Action/delegate
    public Chainsawmancer(LevelObjectManager manager, TextureAtlas atlas, Vector2 position) : base(atlas, position)
    {
        // ? Analyse if the tower owning its sprite/projectileSprite (and multiple chainsawmancers having different sprite instances) is problematic
        Sprite = atlas.CreateSprite("chainsawmancer-1");
        _projectileSprite = atlas.CreateSprite("magicball-1");
        Sprite.CenterOrigin();
        _projectileSprite.CenterOrigin();
        // Sprite.Scale = new Vector2(2.0f, 2.0f);
        Initialize();
    }

    public void Initialize()
    {
        OnTarget += Target;
        Range = 100;
        _snapTime = 500;
        _shootCooldown = 2000;
    }

    /// Updates the state of the projectile tower by targeting nearby enemies within range,
    /// managing the shooting timer, and passing necessary updates to the base tower update logic.
    /// <param name="gameTime">
    /// The current game time, which provides timing data for the update logic.
    /// </param>
    /// <param name="enemies">
    /// A collection of enemies currently active in the game, which are evaluated for being within range of the tower.
    /// </param>
    public override void Update(GameTime gameTime)
    {
        if (OnCooldown)
        {
            _shootTimer = Math.Min(_shootTimer + Core.DT * 1000, _shootCooldown);
            if (_shootTimer >= _shootCooldown)
            {
                OnCooldown = false;
            }
        }
        base.Update(gameTime);
    }
    
    public virtual void Target(Vector2 target)
    {
        _currentTarget.X = target.X;
        _currentTarget.Y = target.Y;
        
        float adj = _currentTarget.X - Position.X;
        float opp = _currentTarget.Y - Position.Y;
        
        /*// TODO: Implement a smoother angle snap for the tower
        _startingAngle = Sprite.Rotation;
        _destinationAngle =  (float) Math.Atan2(opp, adj) + MathHelper.PiOver2;*/
        Sprite.Rotation = (float) Math.Atan2(opp, adj) + MathHelper.PiOver2; //Sprite is facing up not left
        
        Vector2 projectileDirection = Vector2.Normalize(new Vector2(adj, opp));
        Vector2 projectilePosition = Position + (projectileDirection * 15);
        
        _shootTimer %= _shootCooldown;
        OnCooldown = true;
        
        Shoot?.Invoke(this ,_projectileSprite, projectilePosition, projectileDirection);
    }
    public override void Draw(GameTime gameTime)
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }
    }
    public Circle GetRange()
    {
            
        Circle bounds = new Circle(
            (int)(Position.X + Sprite.Origin.X),
            (int)(Position.Y + Sprite.Origin.Y),
            (int)(Range)
        );

        return bounds;
    }

    
}