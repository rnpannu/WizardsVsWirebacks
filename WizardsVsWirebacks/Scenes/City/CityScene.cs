using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes.City;


/// <summary>
/// This class is the main game loop for the "City" Level. 
/// Declares objects of all other City-related classes. 
/// Interfaces with GUM elements. 
/// Contains the Update - Draw Loop. 
/// </summary>
public class CityScene : Scene
{   

    // Foundational objects / patterns
    private enum GameState
    {
        Playing,
        Paused
    }

    // Instances of city classes.
    private CityInputManager _input;
    private CityObjectManager _objManager;
    private CityState _state;
    private CityScreen _ui; // Note: This is an interface with GUM.
    

    // Assets
    private Texture2D _background;


    /* Unused Variables:
    private CityState _gameState;
    public static TextureAtlas _atlas;
    public static Tilemap _tileMap;
    private Sprite _buildingIcon;

    // Debug
    private const float _printDelay = 2000f;
    private float _pdCounter = 0;
    */

    private void InitializeUi()
    {
        GumService.Default.Root.Children.Clear();
        _ui = new CityScreen();
        _ui.BuildingIconPushed += HandleBuildingSelected;
        _ui.AddToRoot();
    }
    public override void Initialize()
    {
        InitializeUi();
        CityConfig.TileSize = 16;
        CityConfig.WorldScale = 0.5f;
        Core.ExitOnEscape = false;
        
        _input = new CityInputManager();
        _state = new CityState(_input);
        _objManager = new CityObjectManager(_input, _state);
        
        base.Initialize(); // ALWAYS CALL THIS AT THE END
    }
    
    /// <summary>
    /// Load game assets
    /// </summary>
    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("Tilemaps/City/simplified/Level_0/Background");
        _state.LoadContent();
        _objManager.LoadContent();
    }

    public void HandleBuildingSelected(object sender, BuildingSelectedEventArgs e)
    {
        _objManager.BuildingIconPushed = e.Building;
    }

    /// <summary>
    /// Core gameloop's logical update run 60 times a second
    /// </summary>
    /// <param name="gameTime"> Holds the time state of a game, we will just use Core.DT 99% of the time </param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (GameController.Exit()) // Change to input handl
        {
            Core.ChangeScene(new TitleScene());
        }
        
        _input.Update();
        _objManager.Update();
        _state.Update();
        GumService.Default.Update(gameTime);
    }
    /// <summary>
    /// Core gameloop's call to draw the objects to the screen
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Draw(GameTime gameTime)
    {

        // Clear previous frame's data
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        // Use nearest neighbor sampling when upscaling the art with PointClamp, and apply camera, city, and resolution translations
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _input.GetTransform()); 


        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
        
        _objManager.Draw();
        
        _state.Draw();
        
        Core.SpriteBatch.End(); 
        
        GumService.Default.Draw();
        base.Draw(gameTime);
    }



}

