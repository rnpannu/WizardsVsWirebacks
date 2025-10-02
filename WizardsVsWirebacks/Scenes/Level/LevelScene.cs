using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;

namespace WizardsVsWirebacks.Scenes;

public class LevelScene : Scene
{
    
    private float _printDelay = 2000f;
    private float _pdCounter = 0;

	private Level _level;
    private enum GameState
    {
        Playing,
        FastForward,
        WavePause,
        Paused
    }

    private GameState _gameState;

    private Texture2D _background;
    private Vector2 _position = Vector2.Zero;

    
    private int _currency;

    
    private void InitializeUi()
    {
        GumService.Default.Root.Children.Clear();
        /*_ui = new ();
        _ui.AddToRoot();*/
    }
    
    public override void Initialize()
    {
        InitializeUi();
        _level = new Level();

        base.Initialize();
    }

    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("Tilemaps/City/simplified/Level_1/_bg");

    }


    private void PrintDebugHelper(object dataStructure)
    {
        if (_pdCounter > _printDelay)
        {
            // * Create debug interface, massive switch for controlling console output?
            // * the non vim mfs could just use a debugger but we have to consider their kind
            Console.Out.WriteLine(dataStructure.ToString());
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }
    }
    public override void Update(GameTime gameTime)
    {
        if (GameController.MoveUp()) _position.Y--;
        if (GameController.MoveDown()) _position.Y++;
        if (GameController.MoveLeft()) _position.X--;
        if (GameController.MoveRight()) _position.X++;
    
        GumService.Default.Update(gameTime);
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        // Use nearest neighbor sampling when upscaling the art with PointClamp, and apply camera, city, and resolution translations
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp
            , transformMatrix: Matrix.CreateScale(1.25f)
            ); 
        Core.SpriteBatch.Draw(_background, _position, Color.White);
        Core.SpriteBatch.End(); 
        
        GumService.Default.Draw();
        base.Draw(gameTime);
    }
    
}
