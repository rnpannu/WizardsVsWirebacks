using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardsVsWirebacks.Scenes;

namespace WizardsVsWirebacks;

class Camera
{
    public Vector2 _position;
    public Vector2 _origin;
    public Matrix _transform;
    public float _delay = 1.0f;

    private const float SPEED = 500;
    private Vector2 _minPos, _maxPos;

    private Texture2D _focusPoint; // yes this is ignoring the sprite / texture region setup. but this is only 1 object so... 

    public Matrix Translation { get; set; }
    
    public Camera(Vector2 position)
    {
        _position = position;
        _focusPoint = Core.Content.Load<Texture2D>("images\\focusPoint");
        _origin = new Vector2(_focusPoint.Width / 2, _focusPoint.Height / 2);
    }

    public void SetBounds()
    {

        Console.Out.WriteLine("Outbuffer: " + CityScene.CityTileSize.ToString() + " Inbuffer: " + _origin.X.ToString());
        _minPos = new Vector2((CityScene.CityTileSize / 2) + _origin.X, (CityScene.CityTileSize / 2) + _origin.Y); // half a tile out + half the sprite in?
        _maxPos = new Vector2(CityScene.CityWidthPx  - (CityScene.CityTileSize / 2) - _origin.X, CityScene.CityHeightPx - (CityScene.CityTileSize / 2) - _origin.Y); // makes no sense
    }

    public void CalculateTranslation()
    {
        // Clamping is currently disabled as map size = window size and the camera would just not move at all

        float dx = (Core.Width / 2) - _position.X;
        //dx = MathHelper.Clamp(dx, -CityScene.CityWidthPx + Core.Width + (CityScene.CityTileSize / 2), CityScene.CityTileSize / 2); // Cleaning up this code wouldn't be a bad idea, handling of variables between Core, GameManager, CityScene, and Camera.
        float dy = (Core.Height / 2) - _position.Y;
        //dy = MathHelper.Clamp(dy, -CityScene.CityHeightPx + Core.Height + (CityScene.CityTileSize / 2), CityScene.CityTileSize / 2);
        Translation = Matrix.CreateTranslation(dx, dy, 0f);
    }
    public void Update()
    {
        _position += CityInputManager.CameraDirection * GameManager.DT * SPEED;

        _position = Vector2.Clamp(_position, _minPos, _maxPos);
        CalculateTranslation();
    }

    public void Draw()
    {
        Core.SpriteBatch.Draw(_focusPoint, _position, null, Color.White, 0.0f, _origin, 1.0f, SpriteEffects.None, 0f);
    }

}

