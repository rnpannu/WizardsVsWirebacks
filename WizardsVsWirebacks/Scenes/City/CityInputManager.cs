using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks.Scenes;

public class CityInputManager
{
    
    private float _printDelay = 2000f;
    private float _pdCounter = 0;
    
    public float _delay = 1.0f;
    private const float SPEED = 300;
    
    private Texture2D _focusPoint;
    public Vector2 _origin;
    
    private Vector2 _startingPos;
    public Vector2 _cameraPosition;
    private Vector2 _minPos, _maxPos;
    
    private Vector2 _cameraDirection;
    public Vector2 CameraDirection => _cameraDirection;
    
    public Matrix Translation { get; set; }

    public Matrix Scale { get; set; } = Matrix.CreateScale(CityScene.CityWorldScale);

    
    
    private int _cursorPosX;
    private int _cursorPosY;
    
    public Vector2 MouseCoordsWorld { get; private set; } = Vector2.Zero;
    public Vector2 MouseCoordsScreen { get; private set; } = Vector2.Zero;
    
    public void SetCameraBounds()
    {
        // Console.Out.WriteLine("Outbuffer: " + CityScene.CityTileSize.ToString() + " Inbuffer: " + _origin.X.ToString());
        // idk what i was on when writing this
        _minPos = new Vector2(_origin.X, _origin.Y);
        _maxPos = new Vector2(CityScene.CityWidthPx - _origin.X, CityScene.CityHeightPx - _origin.Y); 
    }

    public CityInputManager()
    {
        Initialize();
    }
    public void Initialize()
    {
        Matrix invert = Matrix.Invert(Core.Scale * Matrix.CreateScale(CityScene.CityWorldScale));
        _startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2, Core.VirtualHeight / 2), invert);
        _cameraPosition = _startingPos;
        
        _focusPoint = Core.Content.Load<Texture2D>("images/focusPoint");
        _origin = new Vector2(_focusPoint.Width / 2, _focusPoint.Height / 2);
        SetCameraBounds();
    }
    public void HandleInput()
    {
        if (GameController.Exit())
        {
            Core.ChangeScene(new TitleScene());
        }
        _cameraDirection = Vector2.Zero;
        
        if (GameController.MoveUp()) _cameraDirection.Y--;
        if (GameController.MoveDown()) _cameraDirection.Y++;
        if (GameController.MoveLeft()) _cameraDirection.X--;
        if (GameController.MoveRight()) _cameraDirection.X++;

        if(_cameraDirection != Vector2.Zero)
        {
            _cameraDirection.Normalize();
        }

        MouseCoordsWorld = Vector2.Transform(GameController.MousePosition().ToVector2(), Matrix.Invert(CityScene.Transform)); // OOP Hell
        

        if (_pdCounter > _printDelay)
        {
            // * Create debug interface, massive switch for controlling console output?
            // * the non vim mfs could just use a debugger but we have to consider their kind
            Console.Out.WriteLine(MouseCoordsWorld.ToString());
            Console.Out.WriteLine("---------------------------------------------------------------");
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }
        
    }
    
    public void CalculateTranslation()
    {
        // TODO: Incorporate a delay / camera smoothing to the camera navigation - current system looks a little jagged as it perfectly follows WASD movement
        // to do the delay define a constant and multiply it by delta time.
        //    - https://youtu.be/YJB1QnEmlTs?si=OVT4WDeNhagwLxVe
        // Also, find out why there is a smaller sliver exposed on the bottom of the city
        
        float dx = _startingPos.X - _cameraPosition.X;
        dx = MathHelper.Clamp(dx, -(_startingPos.X + (_focusPoint.Width * CityScene.CityWorldScale) + _origin.X) - (Core.Width), 0); // Cleaning up this code wouldn't be a bad idea, handling of variables between Core, GameManager, CityScene, and Camera.
        float dy = _startingPos.Y - _cameraPosition.Y;
        dy = MathHelper.Clamp(dy, -(_startingPos.Y + _focusPoint.Height + _origin.Y) - (Core.Height), 0);
        
        
        Translation = Matrix.CreateTranslation(dx, dy, 0f);
        //Translation = Matrix.Identity;
        
        /*if (_pdCounter > _printDelay)
        {
            // * Create debug interface, massive switch for controlling console output?
            // * the non vim mfs could just use a debugger but we have to consider their kind
            Console.Out.WriteLine("X: "+ _cursorPosX.ToString() + ". Y: " + _cursorPosY.ToString());
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }*/
    }
    


    public void Update()
    {
        HandleInput();
        _cameraPosition += ((_cameraDirection * GameManager.DT * SPEED));
        
        _cameraPosition = Vector2.Clamp(_cameraPosition, _minPos, _maxPos);
        
        CalculateTranslation();
    }

    public void Draw()
    {
        Core.SpriteBatch.Draw(_focusPoint, _cameraPosition, null, Color.White, 0.0f, _origin, 1.0f, SpriteEffects.None, default);
    }
}