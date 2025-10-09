using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using WizardsVsWirebacks.GameObjects.Enemies;

namespace WizardsVsWirebacks.Scenes;

public class LevelObjectManager
{
    private Wave _wave;


    public LevelObjectManager()
    {
        _wave = new Wave();
    }
    public void Initialize()
    {
        _wave.Initialize();
        LoadContent();
    }

    public void LoadContent()
    {
        _wave.LoadContent();
    }
    
    public void Update()
    {
        _wave.Update();
    }

    public void Draw()
    {
        _wave.Draw();
    }
}