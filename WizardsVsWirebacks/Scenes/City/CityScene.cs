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
/// Manages the gameloop and objects involved with the city building gameplay
/// </summary>
public class CityScene : Scene
{   
    // TODO: todo, have to do
    // ! Urgent, breaking the system
    // ? Question
    // * High priority, important fucntionality
    
    
    // Utility variables 
    private const float _printDelay = 2000f;
    private float _pdCounter = 0;
    
    // Exposed statics
    public static float CityWorldScale { get; private set; } = 2.0f; // ! Very important to understand, and also discuss
    public static int CityTileSize { get; private set; } // ? true 16, or scaled 32 ?
    public static int CityWidth { get; private set; }
    public static int CityHeight { get; private set; }
    public static int CityWidthPx => CityTileSize * CityWidth; // How do these lambdas impact performance with every frame calculations, what does the compiler optimize?
    public static int CityHeightPx => CityTileSize* CityHeight;
    public static Matrix Transform => _input.Translation * Matrix.CreateScale(CityWorldScale) * Core.Scale;
    
    
    // Foundational objects / patterns
    private enum GameState
    {
        Playing,
        Paused
    }

    private GameState _gameState;
    
    // TODO (UI): Add a pause screen complete with exit to main menu and a button to another options panel
    // Next, implement an options panel.
    // Also make all gum elements independent of screen size (relative not absolute)
    // Also hide the city buildings panel behind a button like monkey city.
    private CityScreen _ui;

    private static CityInputManager _input;
    
    // ? Ongoing, how to best use these?
    private Tilemap _tileMap;
    private TextureAtlas _atlas;
    
    // TODO: Design an extensible building management helper. New class(es)?
    /*private Dictionary<int, Building> _iconMap = new Dictionary<int, Building>();
    private List<Building> _buildings;*/
    
    //private Building _building;
    
    private Sprite _buildingIcon;
    
    private Texture2D _background;
    // 1:1 with IntGrid csv file
    private int[,] _captureGrid;
    
    // Intgrid color map for visualization
    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();
    
    // Core game properties
    public static int Doubloons { get; set; } = 500;


    /// <summary>
    /// Have gum set CityScreen as the current root
    /// </summary>
    private void InitializeUi()
    {
        GumService.Default.Root.Children.Clear();
        _ui = new CityScreen();
        _ui.AddToRoot();
    }

    public override void Initialize()
    {
        base.Initialize();
        CityTileSize = 16;
        Core.ExitOnEscape = false;
        InitializeUi();
        _input = new CityInputManager();
    }


    // 2. Parse intgrid csv file 
    private void LoadIntGrid()
    {
        
        _captureStatusVisual.Add(1, Color.Green); // Captured, add to dictionary definitions
        _captureStatusVisual.Add(2, Color.Red); // Uncaptured 

        // Create a copy of the csv (filepath changes with OS)
        string basePath = "";
        if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
        {
            basePath = "../../../Content/Tilemaps/City/simplified/Level_0/";
            
        } else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
        {
            basePath = Path.Combine("Content", "Tilemaps", "City", "simplified", "Level_0");
        }
        
        string cityFile = Path.Combine(basePath, "City.csv");
        string coreSaveFile = Path.Combine(basePath, "CoreSave.csv");
        string[] lines = File.ReadAllLines(cityFile);

        if (!File.Exists(coreSaveFile))
        {
            Console.Out.WriteLine("Create ");
            File.Copy(cityFile, coreSaveFile, true);
        }
        else Console.Out.WriteLine("Save already exists");
        

        // * Parse csv -> Could improve with serialization / deserialization (Streams) - make more robust
        
        CityHeight = lines.Length; // Lines run across width, total number of lines == height of city in tiles
        for (int i = 0; i < CityHeight; i++)
        {
            //Console.Out.WriteLine(lines[i]);
            int[] rowData = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            
            // first pass
            if (_captureGrid == null)
            {
                CityWidth = rowData.Length;
                Console.Out.WriteLine("Create grid with dimensions: " + CityWidth.ToString() + ", " + CityHeight.ToString());
                _captureGrid = new int[CityWidth, CityHeight];
            }
            for (int j = 0; j < CityWidth; j++)
            {
                _captureGrid[j, i] = rowData[j];
            }
        }
        
    }
    
    /// <summary>
    /// Load game assets
    /// </summary>
    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("Tilemaps/City/simplified/Level_0/Background");
        _atlas = TextureAtlas.FromFile(Content, "images/objectAtlas-definition.xml");
        _buildingIcon = _atlas.CreateSprite("buildingIcon-1");
        _buildingIcon.Scale = new Vector2(1.0f / CityWorldScale, 1.0f / CityWorldScale);

        LoadIntGrid();
    }

    /// <summary>
    /// Redundant, to be encapsulated in separate class
    /// </summary>
    private void HandleInput()
    {


        /*if (GameController.M1Clicked() && _captureGrid[CursorTileX, CursorTileY] == 2) {
          Core.ChangeScene(new LevelScene());
        }*/

        
        if (_ui.BuildingIconPushed)
        {
            
            if (GameController.M1Released())
            {
                //Vector2 releasePosition = new Vector2(CursorTileX, CursorTileY);
                //Console.Out.WriteLine("Drag and drop at position: " + releasePosition.ToString());
                
                // * stuff to do here
                //building = new Building();
                //buildings.Add(building);

                _ui.BuildingIconPushed = false;
            }

        }
        
    }

    /// <summary>
    /// Core gameloop's logical update run 60 times a second
    /// </summary>
    /// <param name="gameTime"> Holds the time state of a game, we will just use Core.DT 99% of the time </param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        HandleInput();
        _input.Update();
        
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
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: (_input.Translation 
             * (Matrix.CreateScale(CityWorldScale) * Core.Scale)));
        
        // Draw static png background (grass)
        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
        
        // Highlight the tile the cursor is currently hovering on
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });

        // ` Might be worth putting in a CursorTileXPx property?
        
        int CursorTileX = Math.Max(0, Math.Min((int)_input.MouseCoordsWorld.X, CityWidthPx - 1)) / CityTileSize;
        int CursorTileY = Math.Max(0, Math.Min((int)_input.MouseCoordsWorld.Y, CityHeightPx - 1)) / CityTileSize;
        
        Rectangle highlightRect = new Rectangle(CursorTileX * CityTileSize, CursorTileY * CityTileSize, CityTileSize, CityTileSize);
        if (_pdCounter > _printDelay)
        {
            // * Create debug interface, massive switch for controlling console output?
            // * the non vim mfs could just use a debugger but we have to consider their kind
            Console.Out.WriteLine(new Vector2(CursorTileX, CursorTileY).ToString());
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }
        // If dragging and dropping building draw building icon instead
        if (_ui.BuildingIconPushed)
        {
            // ? Generate overloads for draw calls?
            _buildingIcon.Color = Color.White * 0.5f; 
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, Color.White * 0.5f);
            _buildingIcon.Draw(Core.SpriteBatch, new Vector2(CursorTileX * CityTileSize, CursorTileY * CityTileSize + 1));
            
        // TODO: Handle Drag & Drop + Building creation
        } else
        {
            Color currentCol = _captureStatusVisual[_captureGrid[CursorTileX, CursorTileY]];
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
        }
        
        // Draw camera sprite placeholder

        _input.Draw();

        Core.SpriteBatch.End();

        GumService.Default.Draw();

        base.Draw(gameTime);
    }
    //4. Show every intgrid entry
    public void HightlightIntgridCells()
    {

        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
        for (int i = 0; i < CityHeight; i++)
        {
            for (int j = 0; j < CityWidth; j++)
            {
                Rectangle highlightRect = new Rectangle(j * CityTileSize, i * CityTileSize, CityTileSize, CityTileSize);
                //Console.Out.WriteLine("Grid position" + j.ToString() + ", " + i.ToString());
                Color currentCol = _captureStatusVisual[_captureGrid[j, i]];
                Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.3f);
            }
        }
    }

}

