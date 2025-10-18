using Microsoft.Xna.Framework;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.Scenes;

/// <summary>
/// A sort of test class. City input manager was an instantiated class that was passed around to whoever needed it.
/// Let's make this one static and see if it causes any problems later down the road
/// </summary>
public static class LevelInputManager
{
    public static Matrix GetTransform()
    {
        // TODO: Figure out scaling, panel UI (not have it over world but rather its own thing)
        // Also figure out fidelity 320x180 or 640x320
        return Matrix.CreateScale(1.25f) 
               //* Core.Scale
               ;
    }

    public static Vector2 GetMouseCoords()
    {
        return Vector2.Transform(GameController.MousePosition().ToVector2(), Matrix.Invert(GetTransform())); 
    }
    
    public static bool Select()
    {
        return GameController.M1Clicked();
    }
    public static bool Drop()
    {
        return GameController.M1Released();
    }
}