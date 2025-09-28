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

public class CityScene : Scene
{   
    // TODO: todo, have to do
    // ! Urgent, breaking the system
    // ? Question
    // * High priority, important functionality
    
    // Foundational objects / patterns
    private enum GameState
    {
        Playing,
        Paused
    }
    private CityState _gameState;
    private CityScreen _ui;
    private CityInputManager _input;
    private CityObjectManager _objManager;
    private CityState _state;
    
    // ? Ongoing, how to best use these?
    public static TextureAtlas _atlas;
    //public static Tilemap _tileMap;
    
    // Assets
    private Sprite _buildingIcon;
    private Texture2D _background;
    
    
    // Debug
    private const float _printDelay = 2000f;
    private float _pdCounter = 0;


    private void InitializeUi()
    {
        GumService.Default.Root.Children.Clear();
        _ui = new CityScreen();
        _ui.AddToRoot();

    }
    public override void Initialize()
    {
        InitializeUi();
        CityConfig.TileSize = 16;
        CityConfig.WorldScale = 0.5f;
        Core.ExitOnEscape = false;
        _ui.BuildingIconPushed += HandleBuildingSelected;
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
        
        _input.Update();
        _objManager.Update();
        GumService.Default.Update(gameTime);
    }
    /// <summary>
    /// Core gameloop's call to draw the objects to the screen
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Draw(GameTime gameTime)
    {
        int width = CityConfig.WidthPx;
        int height = CityConfig.HeightPx;
        int tileSize = CityConfig.TileSize;
        
        // Clear previous frame's data
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        // Use nearest neighbor sampling when upscaling the art with PointClamp, and apply camera, city, and resolution translations
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _input.GetTransform()); 

        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
        
        _objManager.Draw();
        
        _state.Draw();
        // If dragging and dropping building draw building icon instead
        
        /*if (_input.BuildingIconPushed() > 0)
        {
            _buildingIcon.Color = Color.White * 0.5f; 
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, Color.White * 0.3f);
            _buildingIcon.Draw(Core.SpriteBatch, new Vector2(cursorTileX * tileSize, cursorTileY * tileSize + 1));
        } else // Highlight the tile the cursor is currently hovering on
        { */

        //}
        
        Core.SpriteBatch.End(); 
        
        GumService.Default.Draw();
        base.Draw(gameTime);
    }



}

