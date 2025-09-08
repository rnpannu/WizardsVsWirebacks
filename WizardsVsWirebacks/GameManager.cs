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

// THIS IS THE GAME 1 CLASS


// TODO: Scene management, UI management, tile drag and drop
namespace WizardsVsWirebacks
{
    public class GameManager : Core
    {

        GumService GumUI => GumService.Default;

        public static GraphicalUiElement Root;



        public GameManager() : base("Wizards vs Wirebacks", 1280, 720, false)
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
        }



}

