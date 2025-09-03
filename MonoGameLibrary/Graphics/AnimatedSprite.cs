﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics;

public class AnimatedSprite : Sprite
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

    
    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            Region = _animation.Frames[0];
        }
    }

    public AnimatedSprite()
    {

    }

    public AnimatedSprite(Animation animation)
    {
        _animation = animation;
        Region = _animation.Frames[0];
    }

    public void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if(_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if(_currentFrame >= _animation.Frames.Count) // Does .Count sum every single time (potential optimization?)
            {
                _currentFrame = 0;
            }
            //Console.Out.WriteLine(_animation.Frames[_currentFrame].SourceRectangle);
            Region = _animation.Frames[_currentFrame];
        }
    }

}
