﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics;

public class Sprite
{
    public TextureRegion Region { get; set; } // Texture + source rectangle

    public Color Color { get; set;} = Color.White;

    public float Rotation { get; set; } = 0.0f;

    public Vector2 Scale { get; set; } = Vector2.One;

    public Vector2 Origin { get; set; } = Vector2.Zero;

    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    public float LayerDepth { get; set; }

    public float Width => Region.Width * Scale.X;

    public float Height => Region.Height * Scale.Y;

    public Sprite()
    {

    }

    public Sprite(TextureRegion region)
    {
        Region = region;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(Region.Width, Region.Height) * 0.5f;

    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Region.Draw(spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch spriteBatch, Rectangle destRect)
    {
        Region.Draw(spriteBatch, destRect, Color);
    }


}
