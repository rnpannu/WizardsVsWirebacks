using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WizardsVsWirebacks;
using WizardsVsWirebacks.Scenes;

public class EnemyGroup
{
    public int StartTime { get; set; }
    public int EnemyType { get; set; }
    public int SpawnCount { get; set; }
    public double SpawnDelay { get; set; }
    
    public static List<EnemyGroup> FromFile(string filepath)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if (!File.Exists(filepath)) throw new FileNotFoundException("Level config file not found", filepath);

        string json = File.ReadAllText(filepath);
        Console.Out.WriteLine(json);
        List<EnemyGroup> config;
        try
        {
            config = JsonSerializer.Deserialize<List<EnemyGroup>>(json);
            if (config == null) throw new JsonException("Failed to deserialize config");
            //return config;
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Failed to parse config file: {filepath}", ex);
        }

        
        return config;
    }


}

