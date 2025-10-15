using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.Scenes.City;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes;

public class LevelScene : Scene
{


	private Level _level;
    private LevelScreen _ui;
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
        _ui = new LevelScreen();
        _ui.TowerIconPushed += HandleTowerSelected;
        _ui.AddToRoot();
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
    
    public void HandleTowerSelected(object sender, TowerSelectedEventArgs e)
    {
        _level.SelectedTower = e.TowerType;
    }
    public override void Update(GameTime gameTime)
    {
        if (GameController.Exit()) // Change to input handle
        {
            Core.ChangeScene(new CityScene());
        }
        _level.Update(gameTime);
        GumService.Default.Update(gameTime);
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: LevelInputManager.GetTransform());

        Core.SpriteBatch.Draw(_background, _position, Color.White);
        _level.Draw(gameTime);
        
        Core.SpriteBatch.End(); 
        
        GumService.Default.Draw();
        base.Draw(gameTime);
    }
    
}
