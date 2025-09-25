using System;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using RenderingLibrary.Graphics;

using System.Linq;
using WizardsVsWirebacks.Components.Controls;
using WizardsVsWirebacks.Scenes;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.Screens
{
    partial class CityScreen
    {
        public bool BuildingIconPushed { get; set; } = false;
        public bool BuildingIconReleased { get; set; } = false;

        public int SelectedTowerType { get; set; } = 0;
        /*public bool IconHovered => BuildingPaneInstance.BuildingButtonIcon1Instance.IsFocused;*/

        partial void CustomInitialize()
        {
            BuildingPaneInstance.BuildingButtonIcon1Instance.Push += (_, _) =>
            {
                
                BuildingIconPushed = true;
                SelectedTowerType = 1;
                Console.Out.WriteLine("Button pushed"); 
            };
            DragonCostBarInstance.Text = CityScene.Doubloons.ToString();
            //ButtonStandardInstance.Click += (_, _) =>
            //{
            //    GumService.Default.Root.Children.Clear();
            //    var screen = new TitleScreen();
            //    screen.AddToRoot();
            //};
        }
    }
}
