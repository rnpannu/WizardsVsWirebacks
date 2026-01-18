// Wizard Class
// This class serves as a pure data representation of our wizard towers.
// The goal is to store wizards in a easily serializable format for saves.
// Currently the class is very bare bones and will need to be modified as we progress.


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
