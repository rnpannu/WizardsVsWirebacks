using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;


namespace WizardsVsWirebacks.GameObjects;

public class Tile
{
    // TODO: Discuss converting these into texturetile units? In monkey city tiles are very small to place paths on them
    private int Width { get; set; } = 32;
    private int Height { get; set; } = 32;
    public TextureRegion Texture { get; set; }
    public string LevelId { get; private set; }
    public bool Captured { get; set; }
    public Vector2 Position { get; set; }
    public TerrainType TerrainType { get; set; }
    
    public Tile(TextureRegion texture, string levelID, bool captureState, Vector2 postion, TerrainType terrainType)
    {
        Texture = texture;
        LevelId = levelID;
        Captured = captureState;
        Position = postion;
        TerrainType = terrainType;
    }




}







