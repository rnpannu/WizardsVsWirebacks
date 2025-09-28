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

namespace WizardsVsWirebacks.Screens;

public class BuildingSelectedEventArgs : EventArgs
{
    public int Building { get; }
    
    public BuildingSelectedEventArgs(int building)
    {
        Building = building;
    }
}

partial class CityScreen
{
    /*public bool BuildingIconPushed { get; set; } = false;
    public bool BuildingIconReleased { get; set; } = false;*/

    public event EventHandler<BuildingSelectedEventArgs> BuildingIconPushed;

    
    public int SelectedTowerType { get; set; } = 0;
    /*public bool IconHovered => BuildingPaneInstance.BuildingButtonIcon1Instance.IsFocused;*/

    partial void CustomInitialize()
    {
        //TODO: Fix
        // DragonCostBarInstance.Text = CityScene.Doubloons.ToString();
        BuildingPaneInstance.BuildingButtonIcon1Instance.Push += (_, _) =>
        {
            BuildingIconPushed?.Invoke(this, new BuildingSelectedEventArgs(0));
        };
        
        //ButtonStandardInstance.Click += (_, _) =>
        //{
        //    GumService.Default.Root.Children.Clear();
        //    var screen = new TitleScreen();
        //    screen.AddToRoot();
        //};
    }
}

