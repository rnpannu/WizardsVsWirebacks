using WizardsVsWirebacks.Core.Run

namespace WizardVsWirebacks.City.Events
{
    public interface IWorldEvent
    {
        void Resolve(RunState runState);
    }
}
