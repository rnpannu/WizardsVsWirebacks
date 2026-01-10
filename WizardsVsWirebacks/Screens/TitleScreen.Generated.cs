//Code for TitleScreen
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using WizardsVsWirebacks.Components.WizardsVsWirebacks;
using WizardsVsWirebacks.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace WizardsVsWirebacks.Screens;
partial class TitleScreen : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("TitleScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new TitleScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(TitleScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("TitleScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public TitleButtonStandard TitleButtonStandardInstance { get; protected set; }
    public ContainerRuntime DemoSettingsMenu { get; protected set; }
    public NineSliceRuntime Background { get; protected set; }
    public ContainerRuntime ButtonContainer { get; protected set; }
    public TitleButtonStandard1 TitleButtonStandard1Instance { get; protected set; }
    public StackPanel StackPanelInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }

    public string TextInstanceText
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public TitleScreen(InteractiveGue visual) : base(visual)
    {
    }
    public TitleScreen()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        TitleButtonStandardInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TitleButtonStandard>(this.Visual,"TitleButtonStandardInstance");
        DemoSettingsMenu = this.Visual?.GetGraphicalUiElementByName("DemoSettingsMenu") as ContainerRuntime;
        Background = this.Visual?.GetGraphicalUiElementByName("Background") as NineSliceRuntime;
        ButtonContainer = this.Visual?.GetGraphicalUiElementByName("ButtonContainer") as ContainerRuntime;
        TitleButtonStandard1Instance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<TitleButtonStandard1>(this.Visual,"TitleButtonStandard1Instance");
        StackPanelInstance = MonoGameGum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<StackPanel>(this.Visual,"StackPanelInstance");
        TextInstance = this.Visual?.GetGraphicalUiElementByName("TextInstance") as TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
