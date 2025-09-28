using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;

namespace WizardsVsWirebacks.Scenes.City;

public class CityObjectManager
{
    private List<Building> _buildings;
    private Sprite _chainsawSprite;

    private CityInputManager _input;
    private CityState _state;
    
    private TextureAtlas _objectAtlas;

    private Dictionary<BuildingType, Sprite> _buildingSprites;
    public int BuildingIconPushed { get; set; } = -1; // What's the difference between this and an evil public instance variable? Who knows! programming rules are so dumb
    public bool BuildingIconReleased { get; set; } = false;

    
    public CityObjectManager(CityInputManager input, CityState _state)
    {
        _input = input;
        Initialize();
    }

    public void Initialize()
    {
        _buildings = new List<Building>();
        _buildingSprites = new Dictionary<BuildingType, Sprite>();

    }

    public void LoadContent()
    {

        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _chainsawSprite = _objectAtlas.CreateSprite("chainsawmancer-1");
        _buildingSprites.Add(BuildingType.Chainsawmancer, _chainsawSprite);
        //_buildingIcon.Scale = new Vector2(1.0f / CityConfig.WorldScale, 1.0f / CityConfig.WorldScale);
    }

    private void LoadSprites()
    {
        
    }
    public void CreateBuilding(int id, Rectangle position)
    {
        var type = (BuildingType) id;
        Console.Out.WriteLine(position.ToString());
        Building building = type switch
        {
            
            BuildingType.Chainsawmancer => new ChainsawmancerBuilding(_chainsawSprite, position),
            _ => throw new ArgumentException($"Unknown building type: {type}")
        };

        _buildings.Add(building);
        Console.Out.WriteLine("Building count: " + _buildings.Count.ToString());
    }
    
    public void Update( )
    {
        if (BuildingIconPushed >= 0)
        {
            //Console.Out.WriteLine("Event recognized");
            if (_input.Drop()) // Second layer after game controller? lol
            {
                Console.Out.WriteLine("Drag and drop at position: " + new Vector2(_input.MouseCoordsWorld.X, _input.MouseCoordsWorld.Y).ToString());
                // TODO: Buildings !  - Confluence?
                Rectangle buildingPosition = new Rectangle(_input.XTilePx, _input.YTilePx, CityConfig.TileSize, CityConfig.TileSize);
                CreateBuilding(BuildingIconPushed, buildingPosition);
                BuildingIconPushed = -1;
                //BuildingIconReleased = true;
            }
        }
        foreach (Building building in _buildings)
        {
            //building.Update();
        }
        
    }

    public void Draw( )
    {
        foreach (Building building in _buildings)
        {
            building.Draw();
        }
        //_buildingSprites[BuildingType.Chainsawmancer].Draw(Core.SpriteBatch, Vector2.Zero);
        _chainsawSprite.Draw(Core.SpriteBatch, Vector2.Zero);
    }

    public void HighlightIntgridCells()
    {
        
    }
    public void GetBuildingSprite()
    {
        
    }
    
}