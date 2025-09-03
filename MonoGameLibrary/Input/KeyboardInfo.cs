﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Input;

public class KeyboardInfo
{
    public KeyboardState PreviousState { get; set; }

    public KeyboardState CurrentState { get; set; }

    public KeyboardInfo()
    {
        PreviousState = new KeyboardState();
        CurrentState = Keyboard.GetState();


    }

    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();
    }

    public bool IsKeyDown(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }
    public bool IsKeyUp(Keys key)
    {
        return CurrentState.IsKeyUp(key);
    }

    public bool WasKeyJustPressed(Keys key)
    {
        return (CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key));
    }

    public bool WasKeyJustReleased(Keys key)
    {
        return (CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key));

    }
}
