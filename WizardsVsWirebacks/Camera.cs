using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardsVsWirebacks;

class Camera
{
    public Vector2 position;
    public Matrix transform;
    public float delay = 1.0f;
    public Camera(Vector2 position)
    {
        this.position = position;
    }

    //public void Follow(Rectangle target)
    //{
    //    float d = 1;
    //    //float d = delay * Drawing.dt;

    //    //position.X += ((target.X - position.X) + Drawing.width / 2) * d;
    //    //position.Y += ((target.Y - position.Y) + Drawing.height / 2) * d;


    //    //position.X = (-target.X + (Drawing.width / 2 - target.Width / 2) * 2)
    //    //    ;

    //    //position.Y = -target.Y + (Drawing.height / 2 - target.Height / 2)
    //    //    ;


    //    //transform = Matrix.CreateTranslation((int)-position.X, (int)-position.Y, 0);

    //}

}

