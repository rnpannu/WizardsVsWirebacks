using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace WizardsVsWirebacks.Scenes.City;

/// <summary>
/// The purpose of class is to highlight each space on a city screen based on if the tile is captured or not. 
/// Currently disabled as the load method is not linked to the initialization method. 
/// </summary>
public class CityState
{
    private CityInputManager _input;
    private int[,] _captureGrid;
    private Dictionary<int, Color> _captureStatusVisual = new Dictionary<int, Color>();

    /* Unused Variables:
    public int Doubloons { get; set; } = 500;
    */

    public CityState(CityInputManager input)
    {
        _input = input;
        _captureStatusVisual = new Dictionary<int, Color>();
        Initialize();
    }

    public void Initialize()
    {
        _input = _input;

    }
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
        
        CityConfig.Height = lines.Length; // Lines run across width, total number of lines == height of city in tiles
        for (int i = 0; i <  CityConfig.Height; i++)
        {
            //Console.Out.WriteLine(lines[i]);
            int[] rowData = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            
            // first pass
            if (_captureGrid == null)
            {
                CityConfig.Width = rowData.Length;
                Console.Out.WriteLine("Create grid with dimensions: " + CityConfig.Width.ToString() + ", " +  CityConfig.Height.ToString());
                _captureGrid = new int[CityConfig.Width,  CityConfig.Height ];
            }
            for (int j = 0; j < CityConfig.Width; j++)
            {
                _captureGrid[j, i] = rowData[j];
            }
        }
    }

    public void LoadContent()
    {
        LoadIntGrid();
    }

    public void Update()
    {
        //TODO: Make capture statuses an enum
        // Implement the placement tile hover. 
        
        // ? Is this proper use of the input manager? Should this be moved to there? idek
        if (_input.Select() && _captureGrid[_input.CursorTileX, _input.CursorTileY] == 2)
        {
            Core.ChangeScene(new LevelScene());
        }
    }

    public void HighlightHoveredTile()
    {
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
        Rectangle highlightRect = new Rectangle(_input.XTilePx, _input.YTilePx, CityConfig.TileSize, CityConfig.TileSize);
        Color currentCol = _captureStatusVisual[_captureGrid[_input.CursorTileX, _input.CursorTileY]];
        Core.SpriteBatch.Draw(pixelTexture, highlightRect, currentCol * 0.5f);
    }
    public void Draw()
    {
        HighlightHoveredTile();
    }
}