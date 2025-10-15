﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes.City;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes;

/// <summary>
/// A class that kinda tookover from city scene to be a godling class. Slightly better I suppose?
/// Kinda violates the single responsibility principle
///     - Handles camera movement
///     - Handles input processing
///     - Handles coordinate transformations
///     - Does debugging
/// </summary>
public class CityInputManager
{
    private float _printDelay = 2000f;
    private float _pdCounter = 0;
    
    private float _delay = 1.0f;
    private const float SPEED = 300; // ! Camera smoothing
    
    private Texture2D _focusPoint;
    private Vector2 _origin;
    
    private Vector2 _startingPos;
    private Vector2 _cameraPosition;
    private Vector2 _minPos, _maxPos;
    
    private Vector2 _cameraDirection;
    public Vector2 CameraDirection => _cameraDirection;
    
    private int _cursorPosX;
    private int _cursorPosY;
    
    public Vector2 MouseCoordsWorld { get; private set; } = Vector2.Zero;

    public int CursorTileX { get; private set; } = 0;
    public int CursorTileY { get; private set; } = 0;
    
    public int XTilePx => CursorTileX * CityConfig.TileSize;
    public int YTilePx => CursorTileY * CityConfig.TileSize;
    
    public CityInputManager()
    {

        Initialize();
    }


    public void Initialize()
    {
        // Background gum stuff

        // Go back to world coords to get starting position
        Matrix invert = Matrix.Invert(Core.Scale * Matrix.CreateScale(CityConfig.WorldScale));
        _startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2, Core.VirtualHeight / 2), invert);
        _cameraPosition = _startingPos;
        
        // asset stuff
        _focusPoint = Core.Content.Load<Texture2D>("images/focusPoint");
        _origin = new Vector2(_focusPoint.Width / 2, _focusPoint.Height / 2);
        
        SetCameraBounds();
    }


    /// <summary>
    /// Define the bounds that the camera (just the sprite, cameraPosition) can go
    /// City edge camera lockout / clamp is defined in CalculateTranslation
    /// </summary>
    public void SetCameraBounds()
    {
        // idk what i was on when writing this
        _minPos = new Vector2(_origin.X, _origin.Y);
        _maxPos = new Vector2(CityConfig.WidthPx - _origin.X, CityConfig.HeightPx - _origin.Y); 
    }
    
    /// <summary>
    /// Kinda the point of the whole class
    /// </summary>
    // * could fragment into different functions for neatness
    public void UpdateCamera()
    {
        // WASD camera movement
        _cameraDirection = Vector2.Zero;
        
        if (GameController.MoveUp()) _cameraDirection.Y--;
        if (GameController.MoveDown()) _cameraDirection.Y++;
        if (GameController.MoveLeft()) _cameraDirection.X--;
        if (GameController.MoveRight()) _cameraDirection.X++;

        if(_cameraDirection != Vector2.Zero)
        {
            _cameraDirection.Normalize();
        }
        
        _cameraPosition += ((_cameraDirection * GameManager.DT * SPEED));
        _cameraPosition = Vector2.Clamp(_cameraPosition, _minPos, _maxPos);
        
    }
    
    /// <summary>
    /// Get the Translation matrix to shift the world by to simulate a camera.
    /// </summary>
    /// <returns> A translation matrix </returns>
    public Matrix CalculateTranslation()
    {
        // TODO: Incorporate a delay / camera smoothing to the camera navigation - current system looks a little jagged as it perfectly follows WASD movement
        // to do the delay define a constant and multiply it by delta time.
        //    - https://youtu.be/YJB1QnEmlTs?si=OVT4WDeNhagwLxVe
        // Also, find out why there is a smaller sliver exposed on the bottom of the city
        
        float dx = _startingPos.X - _cameraPosition.X;
        dx = MathHelper.Clamp(dx, -(_startingPos.X + (_focusPoint.Width * CityConfig.WorldScale) + _origin.X) - (Core.Width), 0); // ? Reduce magic numbers
        float dy = _startingPos.Y - _cameraPosition.Y;
        dy = MathHelper.Clamp(dy, -(_startingPos.Y + _focusPoint.Height + _origin.Y) - (Core.Height), 0);
        
        
        return Matrix.CreateTranslation(dx, dy, 0f);
        // return Matrix.Identity;
    }
    
    /// <summary>
    /// Get the total transformation to go from world space -> screen space. Recall matrix multiplication goes from
    /// right to left. 1 and 2 might need to be swapped ? idk brain is fried
    /// 1. Scale from base resolution to screen resolution
    /// 2. Scale city up to desired zoom
    /// 3. Translate the whole thing by the camera's total offset
    /// </summary>
    /// <returns> The final transformation matrix </returns>
    public Matrix GetTransform()
    {
        return CalculateTranslation() * Matrix.CreateScale(CityConfig.WorldScale) * Core.Scale;
    }
    
    /// <summary>
    /// Move camera, checkbounds, update translation
    /// </summary>
    public void Update()
    {
        UpdateCamera();
        CalculateTranslation();
        UpdateMouse();
    }

    private void UpdateMouse()
    {
        // Apply inverse transform to get from screenCoords (with translation) -> Worldcoords
        MouseCoordsWorld = Vector2.Transform(GameController.MousePosition().ToVector2(), Matrix.Invert(GetTransform())); // OOP Hell
        CursorTileX = Math.Max(0, Math.Min((int) MouseCoordsWorld.X, CityConfig.WidthPx - 1)) / CityConfig.TileSize;
        CursorTileY = Math.Max(0, Math.Min((int) MouseCoordsWorld.Y, CityConfig.HeightPx - 1)) / CityConfig.TileSize;
    }

    public bool Select()
    {
        return GameController.M1Clicked();
    }
    public bool Drop()
    {
        return GameController.M1Released();
    }
    
    
    /// <summary>
    /// Debugging / completely useless
    /// </summary>
    public void Draw()
    {
        Core.SpriteBatch.Draw(_focusPoint, _cameraPosition, null, Color.White, 0.0f, _origin, 1.0f, SpriteEffects.None, default);
    }
    
}