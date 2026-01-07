using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Linq;
using WizardsVsWirebacks.Scenes;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.Screens
{
    partial class TitleScreen
    {
        /// <summary>
        /// Adds methods to interface with Gum UI Objects.
        /// Note Two different ways to change sreens found here.
        /// </summary>
        partial void CustomInitialize()
        {
            TitleButtonStandardInstance.Click += (_, _) =>
            {

                Core.ChangeScene(new CityScene());
            };

            TitleButtonStandard1Instance.Click += (_, _) =>
            {
                GumService.Default.Root.Children.Clear();
                var screen = new DemoScreenGum();
                screen.AddToRoot();
                //Core.ChangeScene(new CityScene());
            }
            ;
        }
    }
}
