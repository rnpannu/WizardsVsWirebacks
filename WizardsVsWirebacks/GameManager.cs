using Gum.Wireframe;
using MonoGameGum;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes;

// TODO: Create autotiling + source tile conversion scheme / engine
namespace WizardsVsWirebacks
{
    // This is Game1 renamed, entry point of the application
    public class GameManager() : Core("Wizards vs Wirebacks", 1600, 900, false)
    {
        private static GumService GumUi => GumService.Default;

        public static GraphicalUiElement Root;

        // Redundant?/Original definition of virtual height
        // Load and link gum project
        private void InitializeGum()
        {
            // var gumproject
            var unused = GumUi.Initialize(
                this,
                "GumProject2/GumProject.gumx");
            
            base.Initialize();
        }

        /// <summary>
        /// Intialize UI and set the active scene as Title
        /// </summary>
        protected override void Initialize()
        {
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

