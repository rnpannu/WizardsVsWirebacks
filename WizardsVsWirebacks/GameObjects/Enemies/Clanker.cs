using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public class Clanker : Enemy
{
    public Clanker(Sprite sprite, Vector2[] waypoints, Vector2 position) : base(sprite, waypoints)
    {
        _movementSpeed = 100;
        // ! Refactor. Every enemy probably does not need its own copy of waypoints 
        // ! Create a object/wave manager that manages the enemies
        _waypoints = waypoints;
        _currentPosition = position;
        _nextPosition = waypoints[0];
    }

    public override void Initialize()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Draw()
    {
        //_sprite.Draw(Core.SpriteBatch, );
    }
}