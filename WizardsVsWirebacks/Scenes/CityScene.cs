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
using System.Text;
using System.Threading.Tasks;
using ToolsUtilities;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes;

public class CityScene : Scene
{
    private const int TILE_SIZE = 16;

    private enum GameState
    {
        Playing,
        Paused
    }

    private GameState _gameState;

    private Camera _camera;

    private Texture2D _background;

    // 1:1 with IntGrid csv file
    private int[,] _captureGrid; 

    // Intgrid color map for visualization
    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();

    // Gum 
    private CityScreen _UI;

    // Not currently used
    private Tilemap _tileMap;
    private TextureAtlas _atlas;

    // Scene globals
    public static int CityTileSize { get; set; }
    public static int CityWidth { get; set; }
    public static int CityHeight { get; set; }
    public static int CityWidthPx => CityTileSize * CityWidth; // How do these lambdas impact performance with every frame calculations, what does the compiler optimize?
    public static int CityHeightPx => CityTileSize * CityHeight;
    

    private int _cursorTileX;
    private int _cursorTileY;


    /// <summary>
    /// ----------------------------- INITIALIZATION Logic -----------------------------------
    /// </summary>
    /// 

    // Gum logic
    private void InitializeUI()
    {
        var screen = new CityScreen();
        screen.AddToRoot();

    }
    public override void Initialize()
    {
        base.Initialize();
        CityTileSize = 16;
        InitializeUI();
        _camera = new Camera(new Vector2(Core.Width / 2, Core.Height / 2));
        _camera.SetBounds();
    }

    /// <summary>
    /// Parse intgrid csv file 
    /// </summary>
    private void LoadIntGrid()
    {
        _captureStatusVisual.Add(1, Color.Green); //Captured
        _captureStatusVisual.Add(2, Color.Red); //Uncaptured

        // Create a copy of the csv
        string basePath = "..\\..\\..\\Content\\Tilemaps\\City\\simplified\\Level_0\\";
        string cityFile = Path.Combine(basePath, "City.csv");
        string coreSaveFile = Path.Combine(basePath, "CoreSave.csv");

        string[] lines = File.ReadAllLines(cityFile);

        if (!File.Exists(coreSaveFile))
        {
            Console.Out.WriteLine("Create ");
            File.Copy(cityFile, coreSaveFile, true);
        }
        else Console.Out.WriteLine("Save already exists");

        // Parse csv -> Could improve with serialization / deserialization (Streams) - make more robust
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
        LoadIntGrid();
    }

    /// <summary>
    /// ----------------------------- UPDATE Logic -----------------------------------
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CityInputManager.Update();
        _camera.Update();
        Point cursor = GameController.MousePosition();

        // Record cursor position
        _cursorTileX = Math.Max(0, Math.Min(cursor.X, Core.Width - 1)) / CityTileSize;
        _cursorTileY = Math.Max(0, Math.Min(cursor.Y, Core.Height - 1)) / CityTileSize;

        GumService.Default.Update(gameTime);
    }


    /// <summary>
    /// ----------------------------- DRAW Logic -----------------------------------
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Translation);

        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);


        // Highlight the tile the cursor is currently hovering on
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });

        
        Rectangle highlightRect = new Rectangle(_cursorTileX * CityTileSize, _cursorTileY * CityTileSize, CityTileSize, CityTileSize);
        Color currentCol = _captureStatusVisual[_captureGrid[_cursorTileX, _cursorTileY]];
        Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);

        // Draw camera sprite placeholder
        _camera.Draw();


        Core.SpriteBatch.End();
        base.Draw(gameTime);
    }

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

