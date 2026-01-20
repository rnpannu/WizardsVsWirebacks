namespace WizardsVsWirebacks.Scenes.City;

/// <summary>
/// Effectively acts as a struct. Holds the dimensions of the screen and tile size.
/// </summary>
public static class CityConfig
{
    // ? Does monkey city allow zooming in
    // ? 640x360 does not integer scale to 1600 x 900 so figure out what's going on
    
    public static float WorldScale { get; set; }
    public static int TileSize { get; set; }
    public static int Width { get; set; }
    public static int Height { get; set; }
    public static int WidthPx => TileSize * Width;
    public static int HeightPx => TileSize * Height;
    

}