//Wizard Class

namespace WizardsVsWirebacks.Wizards
{
    public class Wizard
    {
        public enum WizardType 
        { 
            Pojectile, 
            AreaOfEffect 
        }

        public string Name { get; set; }
        public WizardType Type { get; set; }

        public Wizard(string name, WizardType type)
        {
            Name = name;
            Type = type;
        }
    }
}
