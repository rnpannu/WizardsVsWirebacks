using FlatRedBall.Glue.StateInterpolation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToolsUtilities;
using WizardsVsWirebacks.Screens;
using WizardsVsWirebacks.GameObjects;

namespace WizardsVsWirebacks.Scenes;

public class CityScene : Scene
{   
    // TODO: todo, have to do
    
    // ! Urgent, breaking the system
    // ? Question
    // * High priority, important fucntionality
    
    // TODO: Add a pause screen complete with exit to main menu and a button to another options panel
    // Next, implement an options panel.
    // Also make all gum elements independent of screen size (relative not absolute)
    // Also hide the city buildings panel behind a button like monkey city.
    private enum GameState
    {
        Playing,
        Paused
    }

    private GameState _gameState;

    private Camera _camera;
    private static Vector2 _cameraDirection;
    public static Vector2 CameraDirection => _cameraDirection;
    
    private Texture2D _background;

    // 1:1 with IntGrid csv file
    private int[,] _captureGrid;

    // Intgrid color map for visualization
    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();

    // Gum 
    private CityScreen _UI;

    // ? Ongoing, how to best use these?
    private Tilemap _tileMap;
    private TextureAtlas _atlas;

    // TODO: Design an extensible building management helper. New class(es)?
    /*private Dictionary<int, Building> _iconMap = new Dictionary<int, Building>();
    private List<Building> _buildings;*/
    
    //private Building _building;
    private Sprite _buildingIcon;
    
    // Scene globals
    public static float CityWorldScale { get; set; } = 2.0f;
    public static int CityTileSize { get; set; }
    public static int CityWidth { get; set; }
    public static int CityHeight { get; set; }
    public static int CityWidthPx => CityTileSize * CityWidth; // How do these lambdas impact performance with every frame calculations, what does the compiler optimize?
    public static int CityHeightPx => CityTileSize * CityHeight;
    
    private int _cursorPosX;
    private int _cursorPosY;
    
    /// <summary>
    /// Expression-bodied properties:
    /// The => lambda syntax creates read-only computed properties using expression body syntax(introduced in C# 6.0). These properties:
    /// Have no setter (read-only)
    /// Calculate their values on-demand based on the underlying fields
    /// Are essentially shorthand for { get { return _cursorPosX* CityTileSize; } }
    /// </summary>
    private int CursorTileX => Math.Max(0, Math.Min(_cursorPosX, Core.Width - 1)) / CityTileSize;
    private int CursorTileY => Math.Max(0, Math.Min(_cursorPosY, Core.Height - 1)) / CityTileSize;




    private void InitializeUI()
    {
        _UI = new CityScreen();
        _UI.AddToRoot();
    }

    public override void Initialize()
    {
        base.Initialize();
        CityTileSize = 16;
        InitializeUI();
        _camera = new Camera(new Vector2(Core.Width / 2, Core.Height / 2));
        _camera.SetBounds();
    }


    // 2. Parse intgrid csv file 
    private void LoadIntGrid()
    {
        _captureStatusVisual.Add(1, Color.Green); //Captured
        _captureStatusVisual.Add(2, Color.Red); //Uncaptured 

        // Create a copy of the csv

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

        // ` Parse csv -> Could improve with serialization / deserialization (Streams) - make more robust
        CityHeight = lines.Length;
        for (int i = 0; i < CityHeight; i++)
        {
            //Console.Out.WriteLine(lines[i]);
            int[] rowData = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

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
    
    public override void LoadContent()
    {
        _background = Content.Load<Texture2D>("Tilemaps/City/simplified/Level_0/Background");
        _atlas = TextureAtlas.FromFile(Content, "images/objectAtlas-definition.xml");
        _buildingIcon = _atlas.CreateSprite("buildingIcon-1");
        _buildingIcon.Scale = new Vector2(1.0f / CityWorldScale, 1.0f / CityWorldScale);

        LoadIntGrid();
    }

    private void HandleInput()
    {

        _cameraDirection = Vector2.Zero;
        
        _cursorPosX = GameController.MousePosition().X / (int)CityWorldScale;
        _cursorPosY = GameController.MousePosition().Y / (int)CityWorldScale;
        
        if (GameController.MoveUp()) _cameraDirection.Y--;
        if (GameController.MoveDown()) _cameraDirection.Y++;
        if (GameController.MoveLeft()) _cameraDirection.X--;
        if (GameController.MoveRight()) _cameraDirection.X++;

        if(_cameraDirection != Vector2.Zero)
        {
            _cameraDirection.Normalize();
        }

        if (GameController.M1Clicked() && _captureGrid[CursorTileX, CursorTileY] == 2) {
          Core.ChangeScene(new LevelScene());
        }
        
        if (_UI.BuildingIconPushed)
        {
            
            if (GameController.M1Released())
            {
                Vector2 releasePosition = new Vector2(CursorTileX, CursorTileY);
                Console.Out.WriteLine("Drag and drop at position: " + releasePosition.ToString());
                
                // * stuff to do here
                //building = new Building();
                //buildings.Add(building);

                _UI.BuildingIconPushed = false;
            }

        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        HandleInput();
        _camera.Update();
        
        GumService.Default.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Translation * Matrix.CreateScale(CityWorldScale));

        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);


        // Highlight the tile the cursor is currently hovering on
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });

        // ` Might be worth putting in a CursorTileXPx property?
        Rectangle highlightRect = new Rectangle(CursorTileX * CityTileSize, CursorTileY * CityTileSize, CityTileSize, CityTileSize);


        // If dragging and dropping building draw building icon instead
        if (_UI.BuildingIconPushed)
        {
            // ? Generate overloads for draw calls?
            
            _buildingIcon.Color = Color.White * 0.5f; 
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, Color.White * 0.5f);
            _buildingIcon.Draw(Core.SpriteBatch, new Vector2(CursorTileX * CityTileSize, CursorTileY * CityTileSize + 1));
            
        // TODO: Handle Drag & Drop + Building creation
        } /*else if (_UI.BuildingIconReleased)
        {
            
        }*/
        else
        {
            Color currentCol = _captureStatusVisual[_captureGrid[CursorTileX, CursorTileY]];
            Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
        }


        // Draw camera sprite placeholder
        _camera.Draw();


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
                Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
            }
        }
    }

}

