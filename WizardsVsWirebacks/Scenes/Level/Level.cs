using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.Scenes;

public class Level
{
    private LevelObjectManager _obj;
    public int SelectedTower { get; set; } = -1;
    public Level()
    {
        _obj = new LevelObjectManager();
        Initialize();
    }

    /// <summary>
    /// Load config from JSON file
    /// </summary>

    public void Initialize()
    {
        _obj.Initialize();
        LoadContent();
    }

    private void LoadContent()
    {
        _obj.LoadContent();
    }


    public void Update(GameTime gameTime)
    {
        if (SelectedTower >= 0 && LevelInputManager.Drop())
        {
            // TODO: Buildings !  - Confluence?
            Vector2 location = LevelInputManager.GetMouseCoords();
            _obj.CreateTower(SelectedTower, location);
            SelectedTower = -1;
            //BuildingIconReleased = true;
        }
        _obj.Update(gameTime);
    }
    public void Draw(GameTime gameTime)
    {
        _obj.Draw(gameTime);
    }
}