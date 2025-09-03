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
using WizardsVsWirebacks.Screens;
using WizardsVsWirebacks.Scenes;
using System;

// TODO: Scene management, UI management, tile drag and drop
namespace WizardsVsWirebacks
{
    public class Game1 : Core
    {
        // Setup documentation FLAG: S
        GumService GumUI => GumService.Default;

        // Codegen Video FLAG: C
        public static GraphicalUiElement Root;


        public Game1() : base("Wizards vs Wirebacks", 1280, 720, false)
        {

        }
        public void InitializeGum()
        {
            // S
            var gumProject = GumUI.Initialize(
                this,
                "GumProject2/GumProject.gumx");

            
            base.Initialize();



        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();

            InitializeGum();
            //Audio.PlaySong(_themeSong);

            ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {

            //_themeSong = Content.Load<Song>("audio/theme");
        }

        //public void UpdateUI(GameTime gameTime)
        //{
        //    //S
        //    GumUI.Update(gameTime);

        //    //C
        //    // Global MonoGameGum update - background processes
        //    //GumService.Default.Update(this, gameTime);

        //    // To create a common method on both "Screen" classes we create an interface and then each screen
        //    // implements the interface (MonogameGumScreen) which allows a polymorphic update call
        //    // This is where we put our logic for the keypresses in the screen to keep the logic in there. (strategy pattern)
        //    //var convertedRoot = (IMonoGameGumScreen)Root;
        //    //convertedRoot.Update(gameTime);
        //}
        //protected override void Update(GameTime gameTime)
        //{
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //        Exit();

        //    // TODO: Add your update logic here
        //    UpdateUI(gameTime);
        //    base.Update(gameTime);
        }



}

