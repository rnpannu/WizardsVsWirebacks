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

    private Texture2D _background;

    private int[,] _captureGrid; // 1280 / 16, 720 / 16 I feel like I remember C++ not liking having variables in memory allocs

    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();

    private CityScreen _UI;

    private Tilemap _tileMap;

    private TextureAtlas _atlas;


    private int _cursorTileX;

    private int _cursorTileY;

    private void InitializeUI()
    {
        var screen = new CityScreen();
        screen.AddToRoot();
    }
    public override void Initialize()
    {
        base.Initialize();
        GameManager.CityTileSize = 16;
        InitializeUI();
    }

    private void LoadIntGrid()
    {
        _captureStatusVisual.Add(1, Color.Green); //Captured
        _captureStatusVisual.Add(2, Color.Red); //Uncaptured

        string[] lines = File.ReadAllLines("..\\..\\..\\Content\\Tilemaps\\City\\simplified\\Level_0\\City.csv");

        GameManager.CityHeight = lines.Length;
        for (int i = 0; i < GameManager.CityHeight; i++)
        {
            Console.Out.WriteLine(lines[i]);
            int[] rowData = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            
            if (_captureGrid == null)
            {
                GameManager.CityWidth = rowData.Length;
                Console.Out.WriteLine("Instantiate grid with dimensions: " + GameManager.CityWidth.ToString() + ", " + GameManager.CityHeight.ToString());
                _captureGrid = new int[GameManager.CityWidth, GameManager.CityHeight];
            }
            for (int j = 0; j < GameManager.CityWidth; j++)
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Point cursor = GameController.MousePosition();

        _cursorTileX = cursor.X / GameManager.CityTileSize;
        _cursorTileY = cursor.Y / GameManager.CityTileSize;
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);

        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });

        Rectangle highlightRect = new Rectangle(_cursorTileX * GameManager.CityTileSize, _cursorTileY * GameManager.CityTileSize, GameManager.CityTileSize, GameManager.CityTileSize);
        Color currentCol = _captureStatusVisual[_captureGrid[_cursorTileX, _cursorTileY]];
        Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);

        // ---- Highlight all intgrid cells
        //Texture2D pixelTexture;
        //pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        //pixelTexture.SetData(new[] { Color.White });
        //for (int i = 0; i < GameManager.CityHeight; i++)
        //{
        //    for (int j = 0; j < GameManager.CityWidth; j++)
        //    {
        //        Rectangle highlightRect = new Rectangle(j * GameManager.CityTileSize, i * GameManager.CityTileSize, GameManager.CityTileSize, GameManager.CityTileSize);
        //        //Console.Out.WriteLine("Grid position" + j.ToString() + ", " + i.ToString());
        //        Color currentCol = _captureStatusVisual[_captureGrid[j, i]];
        //        Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
        //    }
        //}


        Core.SpriteBatch.End();
        base.Draw(gameTime);
        
    }

}

