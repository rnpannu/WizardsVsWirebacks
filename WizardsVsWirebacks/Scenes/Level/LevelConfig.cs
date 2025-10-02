using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace WizardsVsWirebacks.Scenes;
public class LevelConfig
{
    public string identifier { get; set; }
    public string uniqueIdentifer { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int width { get; set; }
    public int height { get;  set; }
    public string bgColor { get; set; }
    public NeighbourLevels[] neighbourLevels { get; set; }
    public CustomFields customFields { get; set; }
    public string[] layers { get; set; }
    public Entities entities { get; set; }

    public static LevelConfig FromFile(string filepath)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if (!File.Exists(filepath)) throw new FileNotFoundException("Level config file not found", filepath);

        string json = File.ReadAllText(filepath);
        LevelConfig config;
        try
        {
            config = JsonSerializer.Deserialize<LevelConfig>(json);
            if (config == null) throw new JsonException("Failed to deserialize level config");
            return config;
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Failed to parse level config file: {filepath}", ex);
        }

        return config;
    }
    
}

public class NeighbourLevels
{
    public string levelIid { get; set; }
    public string dir { get; set; }
}

public class CustomFields
{

}

public class Entities
{
    public Waypoints[] Waypoints { get; set; }
}

public class Waypoints
{
    public string id { get; set; }
    public string iid { get; set; }
    public string layer { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int color { get; set; }
    public CustomFields1 customFields { get; set; }


}

public class CustomFields1
{
    public Waypoint[] Waypoint { get; set; }
}

public class Waypoint
{
    public int cx { get; set; }
    public int cy { get; set; }
}




