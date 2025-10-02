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
    private Texture2D _backgroundPattern;

    private Vector2 _backgroundOffset; // scrolling effect

    private Rectangle _backgroundDestination;

    private float _scrollSpeed = 50.0f;

    private TextureAtlas _atlas;

    private SoundEffect _uiSoundEffect;

    private TitleScreen _UI;
    public override void Initialize()
    {
        Core.ExitOnEscape = true;
        _backgroundOffset = Vector2.Zero;
        _backgroundDestination = Core.GraphicsDevice.PresentationParameters.Bounds;
        InitializeUI();
        base.Initialize();
    }

    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        var screen = new TitleScreen();
        screen.AddToRoot();
    }

    public override void LoadContent()
    {
        //_atlas = TextureAtlas.FromFile(Core.Content, "images/atlas-definition.xml");
        _backgroundPattern = Content.Load<Texture2D>("images/background-pattern");
    }
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
