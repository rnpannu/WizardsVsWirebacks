//Code for WizardsVsWirebacks/BuildingPanel/BuildingPane (Container)
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

namespace WizardsVsWirebacks.Components.WizardsVsWirebacks.BuildingPanel;
partial class BuildingPane : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("WizardsVsWirebacks/BuildingPanel/BuildingPane");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new BuildingPane(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(BuildingPane)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("WizardsVsWirebacks/BuildingPanel/BuildingPane", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime Background { get; protected set; }
    public CityScrollBar CityScrollBarInstance { get; protected set; }
    public ContainerRuntime ClipContainerInstance { get; protected set; }
    public ContainerRuntime InnerPanelInstance { get; protected set; }
    public NineSliceRuntime FocusedIndicator { get; protected set; }

    public BuildingPane(InteractiveGue visual) : base(visual)
    {
    }
    public BuildingPane()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as NineSliceRuntime;
        CityScrollBarInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<CityScrollBar>(this.Visual,"CityScrollBarInstance");
        ClipContainerInstance = this.Visual?.GetGraphicalUiElementByName("ClipContainerInstance") as ContainerRuntime;
        InnerPanelInstance = this.Visual?.GetGraphicalUiElementByName("InnerPanelInstance") as ContainerRuntime;
        FocusedIndicator = this.Visual?.GetGraphicalUiElementByName("FocusedIndicator") as NineSliceRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
