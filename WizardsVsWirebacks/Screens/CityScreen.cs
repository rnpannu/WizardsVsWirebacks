using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using RenderingLibrary.Graphics;

using System.Linq;
using WizardsVsWirebacks.Components.Controls;

namespace WizardsVsWirebacks.Screens
{
    partial class CityScreen
    {
        public bool BuildingIconPushed { get; set; } = false;
        public bool BuildingIconReleased { get; set; } = false;
        partial void CustomInitialize()
        {
            BuildingPaneInstance.BuildingButtonIcon1Instance.Push += (_, _) =>
            {
                BuildingIconPushed = true;
            };

            BuildingPaneInstance.BuildingButtonIcon1Instance.Click += (_, _) =>
            {
                BuildingIconReleased = true;
            };
            //ButtonStandardInstance.Click += (_, _) =>
            //{
            //    GumService.Default.Root.Children.Clear();
            //    var screen = new TitleScreen();
            //    screen.AddToRoot();
            //};
        }
    }
}
