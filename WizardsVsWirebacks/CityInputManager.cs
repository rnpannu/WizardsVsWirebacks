using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardsVsWirebacks;

public static class CityInputManager
{
    private static Vector2 _cameraDirection;
    public static Vector2 CameraDirection => _cameraDirection;


    public static void Update()
    {
        _cameraDirection = Vector2.Zero;

        if (GameController.MoveUp()) _cameraDirection.Y--;
        if (GameController.MoveDown()) _cameraDirection.Y++;
        if (GameController.MoveLeft()) _cameraDirection.X--;
        if (GameController.MoveRight()) _cameraDirection.X++;

        if(_cameraDirection != Vector2.Zero)
        {
            _cameraDirection.Normalize();
        }

        if (GameController.ZoomIn())
        {
            //CityScale.
        }
    }


}
