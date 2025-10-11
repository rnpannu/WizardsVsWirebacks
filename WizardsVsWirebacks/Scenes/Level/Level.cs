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


    public void Update()
    {
        _obj.Update();
    }
    public void Draw()
    {
        _obj.Draw();
    }
}