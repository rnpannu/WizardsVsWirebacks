//Code for Controls/SplitterStandard (Container)
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace WizardsVsWirebacks.Components.Controls;
partial class SplitterStandard : MonoGameGum.Forms.Controls.Splitter
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new MonoGameGum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("Controls/SplitterStandard");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new SplitterStandard(visual);
            return visual;
        });
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(SplitterStandard)] = template;
        MonoGameGum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(MonoGameGum.Forms.Controls.Splitter)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("Controls/SplitterStandard", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime NineSliceInstance { get; protected set; }

    public SplitterStandard(InteractiveGue visual) : base(visual)
    {
    }
    public SplitterStandard()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        NineSliceInstance = this.Visual?.GetGraphicalUiElementByName("NineSliceInstance") as NineSliceRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
