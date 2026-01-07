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
    public int Range { get; protected set; }
    protected Vector2 _currentTarget;
    
    protected float _startingAngle;
    protected float _destinationAngle;
    protected float _snapTime;
    
    protected float _shootDelay;
    protected float _shootClock;
    protected bool _shooting;

    private Sprite _projectileSprite;
    public List<Projectile> ActiveProjectiles { get; private set; }
    public event Action<Tower, Sprite, Vector2, Vector2> OnShoot; // event keyword enforces that this is the only object that can invoke or null this Action/delegate
    public Chainsawmancer(LevelObjectManager manager, TextureAtlas atlas, Vector2 position) : base(atlas, position)
    {
        ActiveProjectiles = new List<Projectile>();
        Sprite = atlas.CreateSprite("chainsawmancer-1");
        _projectileSprite = atlas.CreateSprite("magicball-1");
        // Sprite.Scale = new Vector2(2.0f, 2.0f);
        Sprite.CenterOrigin();
        _projectileSprite.CenterOrigin();
        Range = 100;
        _snapTime = 500;
        _shootDelay = 2000;
        Initialize();
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

    /// Updates the state of the projectile tower by targeting nearby enemies within range,
    /// managing the shooting timer, and passing necessary updates to the base tower update logic.
    /// <param name="gameTime">
    /// The current game time, which provides timing data for the update logic.
    /// </param>
    /// <param name="enemies">
    /// A collection of enemies currently active in the game, which are evaluated for being within range of the tower.
    /// </param>
    public override void Update(GameTime gameTime, IEnumerable<Enemy> enemies) // IEnumerable covers lists, queues, enumerable data structures
    {

        Enemy targetEnemy = enemies
            .Where(enemy => this.GetRange().Intersects(enemy.GetBounds()))
            .FirstOrDefault();
        // Might be better to use a state machine?
        // If not shooting and targeting -> shoot (shooting = true) else if projectile despawned -> shooting = false
        
        // ToList creates a copy, so that any timeouts/removals from the original list don't cause a collection modified exception
        // more efficient would be doing it in reverse order so objects are only deleted from the end but who cares
        // TODO: Someone do that
        foreach (var projectile in ActiveProjectiles.ToList()) 
        {
            projectile.Update(gameTime);
        }
        /*if (!_shooting)
        {*/
            if (_shootClock >= _shootDelay) // current condition, replace with projectile despawn (!_activeProjectile)
            {
                if (targetEnemy != null)
                {
                    Target(targetEnemy.GetBounds());
                    // TODO: implement a fast linear interpolation of the angle snap to the target to make it not look so jagged
                    // Shoot(Vector2.Normalize(new Vector2(adj, opp)));
                    // float.Lerp(startAngle, targetAngle, shootClock / (shootCooldown / 5));
                    _shooting = true;
                    _shootClock %= _shootDelay;
                }
                
            }
            else
            {
                _shootClock = Math.Min(_shootClock + Core.DT * 1000, _shootDelay); 
            }
            
        base.Update(gameTime, enemies);
    }
    
    public virtual void Target(Circle target)
    {
        _currentTarget.X = target.X;
        _currentTarget.Y = target.Y;
        float adj = _currentTarget.X - Position.X;
        float opp = _currentTarget.Y - Position.Y;
        
        // TODO: Implement a smoother angle snap for the tower
        _startingAngle = Sprite.Rotation;
        _destinationAngle =  (float) Math.Atan2(opp, adj) + MathHelper.PiOver2;
        Sprite.Rotation = (float) Math.Atan2(opp, adj) + MathHelper.PiOver2; //Sprite is facing up not left
        
        // Spawn projectile.
        Vector2 projectileDirection = Vector2.Normalize(new Vector2(adj, opp));
        Vector2 projectilePosition = Position + (projectileDirection * 15);
        OnShoot?.Invoke(this ,_projectileSprite, projectilePosition, projectileDirection);
        //Projectile firedProjectile = new MagicBall(this, _projectileSprite, projectilePosition, projectileDirection);
        //ActiveProjectiles.Add(firedProjectile);
        
        // Important
        /*firedProjectile.OnTimeout += (sender, e) =>
        {
            ActiveProjectiles.Remove((Projectile)sender);
            _shooting = false;
        };*/
    }
    public virtual void Shoot(Vector2 target)
    {
        // potentially move shooting logic into here
    }

    public override void Draw(GameTime gameTime)
    {
        if (Sprite != null)
        {
            Sprite.Draw(Core.SpriteBatch, Position);
        }

        foreach (var projectile in ActiveProjectiles)
        {
            projectile.Draw();
        }

    }
    
}