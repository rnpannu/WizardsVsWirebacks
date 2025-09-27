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
    // * High priority, important functionality
    
    // Foundational objects / patterns
    private enum GameState
    {
        Playing,
        Paused
    }
    private GameState _gameState;
    private CityConfig _config;
    private CityScreen _ui;
    private CityInputManager _input;
    private CityObjectManager _objManager;
    
    // ? Ongoing, how to best use these?
    public static TextureAtlas _atlas;
    
    // Assets
    private Sprite _buildingIcon;
    private Texture2D _background;
    
    // ? Move to object manager?
    private int[,] _captureGrid;// 1:1 with IntGrid csv file
    // Intgrid color map for visualization
    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();
    //public static Tilemap _tileMap;
    
    // Core game properties
    public static int Doubloons { get; set; } = 500;
    
    // Utility variables 
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
        base.Initialize();
        InitializeUi();
        //CityTileSize = 16;
        Core.ExitOnEscape = false;
        _ui.BuildingIconPushed += HandleBuildingSelected;

        _objManager = new CityObjectManager();
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
        
        int CityHeight = lines.Length; // Lines run across width, total number of lines == height of city in tiles
        int CityWidth = 0;
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
        // ? Scrap csv? Random/procedural city generation ? Tilemap ? JSON ? where do the questions stop?
        _config = new CityConfig(2.0f, 16, CityWidth, CityHeight);
        _input = new CityInputManager(_config);
    }
    
    /// <summary>
    /// Load game assets
    /// </summary>
    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("Tilemaps/City/simplified/Level_0/Background");
        _atlas = TextureAtlas.FromFile(Content, "images/objectAtlas-definition.xml");
        _buildingIcon = _atlas.CreateSprite("buildingIcon-1");
        LoadIntGrid();
        _buildingIcon.Scale = new Vector2(1.0f / _config.WorldScale, 1.0f / _config.WorldScale);
    }

    public void HandleBuildingSelected(object sender, BuildingSelectedEventArgs e)
    {
        _objManager.BuildingIconPushed = e.Building;
        if (_input.Drop()) // Second layer after game controller? lol
        {
            Console.Out.WriteLine("Drag and drop at position: " + new Vector2(_input.MouseCoordsWorld.X, _input.MouseCoordsWorld.Y).ToString());
            // TODO: Buildings !  - Confluence?
            //_objManager.CreateBuilding(e, );
            _objManager.BuildingIconPushed = 0;
            _objManager.BuildingIconReleased = true;
        }

    }

    /// <summary>
    /// Core gameloop's logical update run 60 times a second
    /// </summary>
    /// <param name="gameTime"> Holds the time state of a game, we will just use Core.DT 99% of the time </param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _input.Update();
        
        GumService.Default.Update(gameTime);
    }
    /// <summary>
    /// Core gameloop's call to draw the objects to the screen
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Draw(GameTime gameTime)
    {
        int width = _config.WidthPx;
        int height = _config.HeightPx;
        int tileSize = _config.TileSize;
        
        // Create white rectangle texture
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
        
        // Cursor info
        int cursorTileX = Math.Max(0, Math.Min((int)_input.MouseCoordsWorld.X, width - 1)) / tileSize;
        int cursorTileY = Math.Max(0, Math.Min((int)_input.MouseCoordsWorld.Y, height - 1)) / tileSize;
        int xTile = cursorTileX * tileSize;
        int yTile = cursorTileY * tileSize;
        
        Rectangle highlightRect = new Rectangle(xTile, yTile, tileSize, tileSize);
        
        // Clear previous frame's data
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        // Use nearest neighbor sampling when upscaling the art with PointClamp, and apply camera, city, and resolution translations
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _input.GetTransform()); // ! ----
        
        // Draw static png background (grass)
        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
        
        // If dragging and dropping building draw building icon instead
        
        /*if (_input.BuildingIconPushed() > 0)
        {
            _buildingIcon.Color = Color.White * 0.5f; 
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, Color.White * 0.3f);
            _buildingIcon.Draw(Core.SpriteBatch, new Vector2(cursorTileX * tileSize, cursorTileY * tileSize + 1));
        } else // Highlight the tile the cursor is currently hovering on
        { */
            Color currentCol = _captureStatusVisual[_captureGrid[cursorTileX, cursorTileY]];
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
        //}
        
        Core.SpriteBatch.End(); // ! ----------------------------------------------------------------------------------

        GumService.Default.Draw();
        base.Draw(gameTime);
    }



}

