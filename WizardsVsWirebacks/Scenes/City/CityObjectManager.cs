using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;

namespace WizardsVsWirebacks.Scenes.City;

public class CityObjectManager
{
    private List<Building> _buildings;

    private Sprite _chainsawSprite;

    private Dictionary<BuildingType, Sprite> _iconSprites;
    public int BuildingIconPushed { get; set; } = 0; // What's the difference between this and an evil public instance variable? Who knows! programming rules are so dumb
    public bool BuildingIconReleased { get; set; } = false;

    
    public CityObjectManager()
    {
        _buildings = new List<Building>();
        Initialize();
    }

    public void Initialize()
    {

    }

    public void LoadContent()
    {

    }

    public void CreateBuilding(Building building)
    {
        _buildings.Add(building);
        building.OnPlace();
    }
    
    public void Update( )
    {
        foreach (Building building in _buildings)
        {
            building.Update();
        }
        
    }

    public void Draw( )
    {
        foreach (Building building in _buildings)
        {
            building.Draw();
        }
    }

    public void HighlightIntgridCells()
    {
        
    }
    public void GetBuildingSprite()
    {
        
    }
    
}