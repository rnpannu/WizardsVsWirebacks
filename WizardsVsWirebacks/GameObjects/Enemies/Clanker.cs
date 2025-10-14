using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects.Enemies;

public class Clanker : Enemy
{
    public Clanker(Sprite sprite, Vector2[] waypoints, Vector2 position) : base(sprite, waypoints, position)
    {
        _movementSpeed = 100;
        // ! Refactor. Every enemy probably does not need its own copy of waypoints 
        // ! Create a object/wave manager that manages the enemies

    }

    public override void Update()
    {
        base.Update();
    }
    



}