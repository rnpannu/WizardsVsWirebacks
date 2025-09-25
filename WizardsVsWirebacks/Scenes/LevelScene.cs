using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardsVsWirebacks.Scenes;

public class LevelScene : Scene
{
    private enum GameState
    {
        Playing,
        FastForward,
        WavePause,
        Paused
    }

    private GameState _gameState;

    private Tilemap Tilemap;

    private int _currency;
    
    
    // private Camera _camera;
    //private CitySceneUI _ui;
}
