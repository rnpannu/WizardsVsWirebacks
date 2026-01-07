using Gum.Wireframe;
using MonoGameGum;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes;

// TODO: Create autotiling + source tile conversion scheme / engine
namespace WizardsVsWirebacks
{

    /// <summary>
    /// This is Game1 renamed, entry point of the application.
    /// Inherits from Core.
    /// </summary>
    public class GameManager() : Core("Wizards vs Wirebacks", 1600, 900, false)
    {

        private static GumService GumUi => GumService.Default;

        public static GraphicalUiElement Root;
        
        /// <summary>
        /// Load and link gum project
        /// </summary>
        private void InitializeGum()
        {
            // var gumproject
            var unused = GumUi.Initialize(
                this,
                "GumProject2/GumProject.gumx");
            
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
        
        /// <summary>
        /// No content needs to be loaded in the Game Manager.
        /// !! Redundant?
        /// </summary>
        protected override void LoadContent()
        {

            //_themeSong = Content.Load<Song>("audio/theme");
        }
        }



}

