using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using MonoGameGum;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Linq;

namespace WizardsVsWirebacks.Screens
{
    partial class TitleScreen
    {
        partial void CustomInitialize()
        {
            TitleButtonStandardInstance.Click += (_, _) =>
            {
                GumService.Default.Root.Children.Clear();
                var screen = new CityScreen();
                screen.AddToRoot();
            };

            TitleButtonStandard1Instance.Click += (_, _) =>
            {
                GumService.Default.Root.Children.Clear();
                //Core.Exit();
                //Core.Audio.PlaySoundEffect(null);
            }
            ;
            
            //ButtonStandardInstance.Text = $"Clicked {++clickCount} Time(s)";
        }
    }
}
