using System.Collections.Generic;

namespace WizardsVsWirebacks.Wizards
{
    public class WizardPool
    {
        private List<Wizard> _wizards = new List<Wizard>();

        public IReadOnlyList<Wizard> Wizards => _wizards.AsReadOnly();

        public void AddWizard(Wizard wizard)
        {
            _wizards.Add(wizards);
        }

        public void RemoveWizard(Wizards wizard)
        {
            _wizards.Remove(wizard);
        }
    }
}
