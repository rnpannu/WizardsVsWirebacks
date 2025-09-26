using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.GameObjects;

public class ChainsawmancerBuilding : Building
{
    
    public ChainsawmancerBuilding(Sprite sprite) : base(BuildingType.Chainsawmancer, 100, 1, sprite)
    {
        Sprite = sprite;
        Initialize();
    }

    public void Initialize(Vector2 tile)
    {
        GridPosition = tile;
    }
    


}