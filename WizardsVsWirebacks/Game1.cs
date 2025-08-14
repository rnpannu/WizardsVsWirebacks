using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameLibrary;
//using WizardsVsWirebacks.Screens;
using MonoGameGum.Forms.Controls;

// TODO: Scene management, UI management, tile drag and drop
namespace WizardsVsWirebacks
{
    public class Game1 : Core
    {

        public static GraphicalUiElement Root; 

        public void InitializeGum()
        {

            var gumProject = MonoGameGum.GumService.Default.Initialize(
               this,
               "GumProject/GumProject.gumx"
                );

            // Look up intro/title screen
            Gum.DataTypes.ScreenSave titleScreen = gumProject.Screens.Find(item => item.Name == "TitleScreen");
            // create the title screen graphical ui element and set it as the root
            Root = titleScreen.ToGraphicalUiElement(RenderingLibrary.SystemManagers.Default, addToManagers : true);


            // --- OLD / Code only -----

            //GumService.Default.Initialize(this);

            //GumService.Default.ContentLoader.XnaContentManager = Core.Content;

            //FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);

            //Gum.Forms.Controls.FrameworkElement.TabReverseKeyCombos.Add(
            //    new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Up });

            //FrameworkElement.TabKeyCombos.Add(
            //    new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Down });

            //GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 4.0f;
            //GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 4.0f;

            //GumService.Default.Renderer.Camera.Zoom = 4.0f;

        }

        public void UpdateUI(GameTime gameTime)
        {
            // Global MonoGameGum update - background processes
            GumService.Default.Update(this, gameTime);

            // To create a common method on both "Screen" classes we create an interface and then each screen
            // implements the interface (MonogameGumScreen) which allows a polymorphic update call
            // This is where we put our logic for the keypresses in the screen to keep the logic in there. (strategy pattern)
            //var convertedRoot = (IMonoGameGumScreen)Root;
            //convertedRoot.Update(gameTime);
        }

        public void DrawUI(GameTime gameTime)
        {
            GumService.Default.Draw();
        }
        public Game1() : base("Wizards vs Wirebacks", 1280, 720, false)
        {

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            InitializeGum();
            //Audio.PlaySong(_themeSong);

            //ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {

            //_themeSong = Content.Load<Song>("audio/theme");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // TODO: Add your drawing code here

        //    base.Draw(gameTime);
        //}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //DrawUI();

            base.Draw(gameTime);
        }
    }
}
