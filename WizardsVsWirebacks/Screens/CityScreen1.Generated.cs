//Code for CityScreen1
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using WizardsVsWirebacks.Components.WizardsVsWirebacks.BuildingPanel;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace WizardsVsWirebacks.Screens;
partial class CityScreen1 : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("CityScreen1");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new CityScreen1(visual);
            visual.Width = 0;
            visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(CityScreen1)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("CityScreen1", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public BuildingPane BuildingPaneInstance { get; protected set; }

    public CityScreen1(InteractiveGue visual) : base(visual)
    {
    }
    public CityScreen1()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        BuildingPaneInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<BuildingPane>(this.Visual,"BuildingPaneInstance");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
