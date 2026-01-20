using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;

namespace WizardsVsWirebacks.Scenes.City;

/// <summary>
/// Class to contain information about objects on the screen. 
/// Building types, associated assets such as sprites, locations. 
/// </summary>
public class CityObjectManager
{
    
    private List<Building> _buildings; // Buildings to be rendered on screen.
    private Dictionary<BuildingType, Sprite> _buildingSprites; // Dictionary between the Building enum and appropriate Sprite class.

    // Sprites for each enumerated types. 
    // TODO: In the future, may be a good idea to abstract this.
    private Sprite _chainsawSprite;
    private Sprite _lawyerSprite;
    private TextureAtlas _objectAtlas; // Source of sprite images.


    private CityInputManager _input;
    private CityState _state;
    


    // Gonna call this an event listener. Public variable set by a function in CityInputManager, called by CityScene.
    public int BuildingIconPushed { get; set; } = -1; // What's the difference between this and an evil public instance variable? Who knows! programming rules are so dumb
    public bool BuildingIconReleased { get; set; } = false; // Replaced by the Input.Drop function from our input manager.

    
    public CityObjectManager(CityInputManager input, CityState _state)
    {
        _input = input;
        Initialize();
    }

    /// <summary>
    /// Sets up the dictionaries / lists we'll be using.
    /// </summary>
    public void Initialize()
    {
        _buildings = new List<Building>();
        _buildingSprites = new Dictionary<BuildingType, Sprite>();

    }

    /// <summary>
    /// Loads assets for all building types.
    /// Adds newly created sprites to our dictionary for further reference.
    /// </summary>
    public void LoadContent()
    {

        _objectAtlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _chainsawSprite = _objectAtlas.CreateSprite("chainsawmancer-1");
        _lawyerSprite = _objectAtlas.CreateSprite("lawyer-1");
        _buildingSprites.Add(BuildingType.Chainsawmancer, _chainsawSprite);
        //_buildingIcon.Scale = new Vector2(1.0f / CityConfig.WorldScale, 1.0f / CityConfig.WorldScale);
    }

    /// <summary>
    /// Adds a new building to the list of active buildings on the screen.
    /// </summary>
    /// <param name="id"> Building type id. Eg, all chainsawmancers will be 1. </param>
    /// <param name="position"> Location of building on screen. </param>
    /// <exception cref="ArgumentException"></exception>
    public void CreateBuilding(int id, Rectangle position)
    {
        var type = (BuildingType) id;
        Building building = type switch
        { // Cube building - BuildingType
            BuildingType.Chainsawmancer => new ChainsawmancerBuilding(_chainsawSprite, position),
            BuildingType.Lawyer => new LawyerBuilding(_lawyerSprite, position),
            _ => throw new ArgumentException($"Unknown building type: {type}")
        };

        _buildings.Add(building);
    }
    
    /// <summary>
    /// Tests to see if mouse has clicked on a building. If so, call Create building to add an instance to the list to be rendered.
    /// Note the use of the Building Icon Pushed event listener. This is adjusted in CityInputManager, but the event is actually recorded in CityScene.
    /// </summary>
    public void Update( )
    {
        if (BuildingIconPushed >= 0)
        {
            if (_input.Drop()) // Second layer after game controller? lol
            {
                //Console.Out.WriteLine("Drag and drop at position: " + new Vector2(_input.MouseCoordsWorld.X, _input.MouseCoordsWorld.Y).ToString());
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
    /// <summary>
    /// Draws all buildings in our building list on the screen.
    /// Additionally, draws one chainsaw sprite at the zero vector. Why?
    /// </summary>
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

    private void LoadSprites()
    {
        
    }
    
}