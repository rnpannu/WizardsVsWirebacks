// Run State Class Stub

/// Run State is the central state model of the game.
/// It contains the data needed to save and reproduce the game state,
/// along with methods to modify the data.
///
/// RunState should be a single source of truth for the game.
///

public class RunState
{
    // Run Seed
    public int RngSeed { get; set; }

    //Progression Information
    public int CityIndex { get; set; }

    // Resources
    public int Scrap { get; set; }
    public int ManaCap { get; set; }
    
    // Core Systems
    public WizardPool WizardPool { get; private set; }

    public CityGraph CityGraph { get; set; }
    public int CurrentNodeId { get; set; }

    // Debug / UI
    public List<string> EventLog { get; private set; }

    // Constructor for new runs
    public RunState(int seed)
    {
        RngSeed = seed;

        Scrap = 0;
        ManaCap = 3;

        CityIndex = 0;
        
        WizardPool = new WizardPool();
        EventLog = new List<string>();
    }
}
