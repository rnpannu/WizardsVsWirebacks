using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace WizardsVsWirebacks.Scenes;

public class Level
{
    private LevelConfig _config;

    private Vector2 _startPos;

    private Vector2[] _waypoints;
    public Level()
    {
        Initialize();
    }

    /// <summary>
    /// Load config from JSON file
    /// </summary>
    private void InitializeConfig()
    {
        string dataPath = "";
        if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
        {
            dataPath = "../../../Content/Tilemaps/City/simplified/Level_1";
            
        } else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
        {
            dataPath = Path.Combine("Content", "Tilemaps", "City", "simplified", "Level_1");
        }
        string levelFile = Path.Combine(dataPath, "data.json");

        _config = LevelConfig.FromFile(levelFile);

        var abysmal = _config.entities.Waypoints[0];
        _startPos = new Vector2(abysmal.x, abysmal.y);
        
        var atrocious = _config.entities.Waypoints[0].customFields.Waypoint.Length;
        _waypoints = new Vector2[atrocious];
        for (int i = 0; i < atrocious; i++)
        {
            var apalling = _config.entities.Waypoints[0].customFields.Waypoint[i];
            _waypoints[i] = new Vector2(apalling.cx,
                apalling.cy);
        }
        
        /*Console.Out.WriteLine(_startPos.ToString());
        foreach (var wayp in _waypoints)
        {
            Console.Out.WriteLine(wayp.ToString());
        }*/
    }
    public void Initialize()
    {
        InitializeConfig();
    }
}