using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace WizardsVsWirebacks.Scenes;

public class CityInputManager
{
    
    private float _printDelay = 2000f;
    private float _pdCounter = 0;
    
    public float _delay = 1.0f;
    private const float SPEED = 500;
    
    private Texture2D _focusPoint;
    public Vector2 _origin;
    
    private Vector2 _startingPos;
    public Vector2 _cameraPosition;
    private Vector2 _minPos, _maxPos;
    
    private static Vector2 _cameraDirection;
    public static Vector2 CameraDirection => _cameraDirection;
    
    public Matrix Translation { get; set; }
    
    private int _cursorPosX;
    private int _cursorPosY;
    public int CursorPosX => _cursorPosX;
    public int CursorPosY => _cursorPosY;
    
    
    public void SetCameraBounds()
    {
        // Console.Out.WriteLine("Outbuffer: " + CityScene.CityTileSize.ToString() + " Inbuffer: " + _origin.X.ToString());
        // idk what i was on when writing this
        _minPos = new Vector2(0, 0);
        // ! Relevant 
        _maxPos = new Vector2(CityScene.CityWidthPx, CityScene.CityHeightPx); 
    }

    public CityInputManager()
    {
        Initialize();
    }
    public void Initialize()
    {
        _startingPos = new Vector2(Core.Width / 2.0f, Core.Height / 2.0f) / CityScene.CityWorldScale;
        _cameraPosition = _startingPos;

        _focusPoint = Core.Content.Load<Texture2D>("images/focusPoint");
        _origin = new Vector2(_focusPoint.Width / 2, _focusPoint.Height / 2);
        SetCameraBounds();
    }
    public void HandleInput()
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
        
        /*_cursorPosX = (GameController.MousePosition().X - (int)(_camera.Translation.M41 * CityScene.CityWorldScale)) / (int) CityScene.CityWorldScale;
        _cursorPosY = (GameController.MousePosition().Y - (int)(_camera.Translation.M42 * CityScene.CityWorldScale)) / (int) CityScene.CityWorldScale;*/
        
        _cursorPosX = (GameController.MousePosition().X - (int)(Translation.M41));
        _cursorPosY = (GameController.MousePosition().Y - (int)(Translation.M42));
    }
    
    public void CalculateTranslation()
    {
        // TODO: Incorporate a delay / camera smoothing to the camera navigation - current system looks a little jagged as it perfectly follows WASD movement
        // to do the delay define a constant and multiply it by delta time.
        //    - https://youtu.be/YJB1QnEmlTs?si=OVT4WDeNhagwLxVe
        
        // Clamping is currently disabled as map size = window size and the camera would just not move at all

        float dx = _startingPos.X - _cameraPosition.X;
        //dx = MathHelper.Clamp(dx, -CityScene.CityWidthPx + Core.Width + (CityScene.CityTileSize / 2), CityScene.CityTileSize / 2); // Cleaning up this code wouldn't be a bad idea, handling of variables between Core, GameManager, CityScene, and Camera.
        float dy = _startingPos.Y - _cameraPosition.Y;
        //dy = MathHelper.Clamp(dy, -CityScene.CityHeightPx + Core.Height + (CityScene.CityTileSize / 2), CityScene.CityTileSize / 2);
        
        
        Translation = Matrix.CreateTranslation(dx, dy, 0f);
        //Translation = Matrix.Identity;
        
        if (_pdCounter > _printDelay)
        {
            Console.Out.WriteLine(_cameraPosition.ToString());
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }
    }
    public void Update()
    {
        HandleInput();
        _cameraPosition += ((_cameraDirection * GameManager.DT * SPEED) / CityScene.CityWorldScale
            );
        
        _cameraPosition = Vector2.Clamp(_cameraPosition, _minPos, _maxPos);
        
        CalculateTranslation();
    }

    public void Draw()
    {
        Core.SpriteBatch.Draw(_focusPoint, _cameraPosition, null, Color.White, 0.0f, _origin, 1.0f, SpriteEffects.None, default);
    }
}