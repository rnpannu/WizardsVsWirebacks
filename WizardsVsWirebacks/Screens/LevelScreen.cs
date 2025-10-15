using System;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace WizardsVsWirebacks.Screens
{
    public class TowerSelectedEventArgs : EventArgs
    {
        public int TowerType { get; }
    
        public TowerSelectedEventArgs(int tower)
        {
            TowerType = tower;
        }
    }
    partial class LevelScreen
    {
        public event EventHandler<TowerSelectedEventArgs> TowerIconPushed;
        partial void CustomInitialize()
        {
            BuildingPaneInstance.BuildingButtonIcon1Instance.Push += (_, _) =>
            {
                TowerIconPushed?.Invoke(this, new TowerSelectedEventArgs(0));
            };

        }
    }
}
