using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public abstract class Enemy
{
    protected int _movementSpeed;
    protected Vector2 _currentPosition;
    protected Vector2 _nextPosition;
    protected Vector2 Dir => Vector2.Normalize(_nextPosition - _currentPosition);
    protected Sprite _sprite;
    protected Vector2[] _waypoints;
    protected int _currentWayPoint = 0;
    protected int Health { get; set; }
    
    

    public Enemy(Sprite sprite, Vector2[] waypoints)
    {
        _sprite = sprite;
        _waypoints = waypoints;
        Initialize();
    }

    public virtual void Initialize()
    {
        
    }
    public virtual void LoadContent()
    {
        
    }
    public virtual void Update(){
           
    }

    public virtual void Draw()
    {

    }
}