using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardsVsWirebacks.Scenes;

public class CityScene : Scene
{
    private enum GameState
    {
        Playing,
        Paused
    }

    private GameState _gameState;

    private Tilemap _tileMap;

    private TextureAtlas _atlas;

    



}
