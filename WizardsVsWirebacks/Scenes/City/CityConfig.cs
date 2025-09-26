namespace WizardsVsWirebacks.Scenes.City;

public class CityConfig
{
    // ? Does monkey city allow zooming in
    // ? 640x360 does not integer scale to 1600 x 900 so figure out what's going on
    
    public float WorldScale { get; }
    public int TileSize { get; }
    public int Width { get; }
    public int Height { get; }
    public int WidthPx => TileSize * Width;
    public int HeightPx => TileSize * Height;

    
    public CityConfig(float worldScale, int tileSize, int width, int height)
    {
        WorldScale = worldScale;
        TileSize = tileSize;
        Width = width;
        Height = height;
    }
    
    

}