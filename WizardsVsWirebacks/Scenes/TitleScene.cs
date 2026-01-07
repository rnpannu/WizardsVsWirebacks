using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardsVsWirebacks.Scenes.City;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes;

public class TitleScene : Scene
{
    //Following Parameters are used to add the scrolling background.
    private Texture2D _backgroundPattern; // source pattern
    private Rectangle _backgroundDestination; // entire screen.
    private float _scrollSpeed = 50.0f; // speed of scroll
    private Vector2 _backgroundOffset; // instantanious value for x/y offset.


    // Following parameters are unused, as this screen is very simple.
    private TextureAtlas _atlas;
    private SoundEffect _uiSoundEffect;
    private TitleScreen _UI;


    /// <summary>
    /// sets up escape command, initializes values for offset, calls InitializeUI, calls base.Initialize (where LoadContent is found)
    /// </summary>
    public override void Initialize()
    {
        Core.ExitOnEscape = true;
        _backgroundOffset = Vector2.Zero;
        _backgroundDestination = Core.GraphicsDevice.PresentationParameters.Bounds;
        InitializeUI();
        base.Initialize();
    }

    /// <summary>
    /// Establishes the interface with GUM.
    /// !! Note: Uncertain how the screen items are functioning here. Ask Raj why this is included.
    /// </summary>
    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        var screen = new TitleScreen();
        screen.AddToRoot();
    }

    /// <summary>
    /// Loads scrolling background for title screen.
    /// Note: LoadContent is invoked in the "base.initialize" call in TitleScene.Initialize().
    /// </summary>
    public override void LoadContent()
    {
        //_atlas = TextureAtlas.FromFile(Core.Content, "images/atlas-definition.xml");
        _backgroundPattern = Content.Load<Texture2D>("images/background-pattern");
    }

    /// <summary>
    /// Three major functions: Move to next scene if enter pressed, Increment the background offset, Link to the code in our corresponding GUM files.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
        {
            Core.ChangeScene(new CityScene());
        }

        // Scrolling background
        float offset = _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _backgroundOffset.X -= offset;
        _backgroundOffset.Y -= offset;

        _backgroundOffset.X %= _backgroundPattern.Width;
        _backgroundOffset.Y %= _backgroundPattern.Height;

        GumService.Default.Update(gameTime);
    }

    /// <summary>
    /// Establishes the use of pointwrap to sample the background texture (https://docs.monogame.net/articles/tutorials/building_2d_games/18_texture_sampling/index.html)
    /// Otherwise, it draws the Scrolling background and links to the corresponding draw function in our GUM files.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport;
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap
            , transformMatrix: Core.Scale
            );

        Core.SpriteBatch.Draw(
            _backgroundPattern,
            _backgroundDestination,
            new Rectangle(_backgroundOffset.ToPoint(),
            _backgroundDestination.Size),
            Color.White * 0.5f);

        Core.SpriteBatch.End();
        base.Draw(gameTime);

        GumService.Default.Draw();
    }
}
