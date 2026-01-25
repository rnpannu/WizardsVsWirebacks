using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.UI;

public class Healthbar
{
    private Vector2 _position;
    private Sprite _sprite;
    
    private int _healthPool;
    private int _maxHealth;
    private float _pxToHealthCoefficent;
    private Rectangle _boundingBox;
    private Rectangle _greenRectangle;
    private Rectangle _redRectangle;
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public Sprite Sprite
    {
        get => _sprite;
        private set => _sprite = value;
    }
    
    public int HealthPool
    {
        get => _healthPool;
        set
        {
            _healthPool = Math.Max(0, Math.Min(MaxHealth, value));
            HealthChanged?.Invoke(value);
            if (_healthPool == 0)
            {
                //OnDeath?.Invoke();
            }
        }
    }
    
    public int MaxHealth
    {
        get => _maxHealth;
        private set => _maxHealth = value;
    }


    // Lowkey wrote myself into a corner here, only have 10 pixels to work with
    private float PercentHealth => (float) HealthPool / MaxHealth;

    public Rectangle BoundingBox
    {
        get => _boundingBox;
        set => _boundingBox = value;
    }

    public Rectangle GreenRectangle
    {
        get => _greenRectangle;
        set => _greenRectangle = value;
    }

    public Rectangle RedRectangle
    {
        get => _redRectangle;

    }

    public Action<int> HealthChanged;


    public Healthbar(Vector2 position, int maxHealth)
    {
        HealthChanged += OnHealthChanged;
        MaxHealth = maxHealth;
        HealthPool = MaxHealth;
        Position = position;
        
        
        
        _boundingBox = new Rectangle((int)position.X, (int)position.Y, 10, 2);
        _greenRectangle = new Rectangle(_boundingBox.X ,
            _boundingBox.Y,
            (int) (_boundingBox.Width * PercentHealth ),
            (int) (_boundingBox.Height));

        _redRectangle = new Rectangle(_greenRectangle.X + _greenRectangle.Width, _greenRectangle.Y,
            (int) (_boundingBox.Width * (1 - PercentHealth)), _greenRectangle.Height);
        // Want every healthbar to be 30 pixels?
        _pxToHealthCoefficent = 30 / MaxHealth;
    }

    public void UpdateRectanglePositions(Vector2 rootPosition)
    {
        _boundingBox.X = (int) rootPosition.X;
        _boundingBox.Y = (int) rootPosition.Y;
        _greenRectangle.X = _boundingBox.X;
        _greenRectangle.Y = _boundingBox.Y;
        _redRectangle.X = _greenRectangle.X + _greenRectangle.Width;
        _redRectangle.Y = _greenRectangle.Y;
    }
    public void Update(GameTime gameTime, Vector2 parentPosition)
    {
        UpdateRectanglePositions(parentPosition);
    }
    private void OnHealthChanged(int health)
    {
        _greenRectangle.Width = (int)((PercentHealth * _boundingBox.Width * 0.95));
        _redRectangle.Width = (int)((1 - PercentHealth) * _boundingBox.Width);
    }

    public void Draw(GameTime gameTime)
    {
        Texture2D pixelTexture;
        pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
        Core.SpriteBatch.Draw(pixelTexture, _boundingBox, Color.Black * 0.9f);
        Core.SpriteBatch.Draw(pixelTexture, _greenRectangle, Color.Green * 0.9f);
        Core.SpriteBatch.Draw(pixelTexture, _redRectangle, Color.Red * 0.9f);
    }
    
}