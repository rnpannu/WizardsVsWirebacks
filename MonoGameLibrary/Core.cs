﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;


namespace MonoGameLibrary;

public class Core : Game
{

    // Gets a reference to the core instance
    internal static Core s_instance;

    public static Core Instance => s_instance;

    private static Scene s_activeScene;

    private static Scene s_nextScene;


    // Controls the presentation of graphics
    public static GraphicsDeviceManager Graphics { get; private set; }
    
    // Create graphics resource and perform primitive rendering. Primitive as in general basic or OpenGL primitive?
    public static new GraphicsDevice GraphicsDevice {  get; private set; }

    public static SpriteBatch SpriteBatch;

    public static new ContentManager Content { get; private set; }

    public static InputManager Input { get; private set; }

    public static bool ExitOnEscape { get; set; }

    public static AudioController Audio { get; private set; }
    /// <summary>
    /// Creates a new Core instance.
    /// </summary>
    /// <param name="title">The title to display in the title bar of the game window.</param>
    /// <param name="width">The initial width, in pixels, of the game window.</param>
    /// <param name="height">The initial height, in pixels, of the game window.</param>
    /// <param name="fullScreen">Indicates if the game should start in fullscreen mode.</param>
    public Core(string title, int width, int height, bool fullScreen)
    {
        if (s_instance != null)
        {
            throw new InvalidOperationException("Only a single core instance can be created.");
        }

        s_instance = this;

        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();

        Window.Title = title;

        Content = base.Content;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;

        ExitOnEscape = true;

    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;

        SpriteBatch = new SpriteBatch(GraphicsDevice);

        Input = new InputManager();

        Audio = new AudioController();
    }

    protected override void UnloadContent()
    {
        Audio.Dispose();
        base.UnloadContent();
    }
    protected override void Update(GameTime gameTime)
    {
        Input.Update(gameTime);

        Audio.Update();
        if (ExitOnEscape && Input.Keyboard.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if(s_nextScene != null)
        {
            TransitionScene();
        }

        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);
        }

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        if (s_activeScene != next)
        {
            {
                s_nextScene = next;
            }
        }
    }

    private static void TransitionScene()
    {
        if(s_activeScene != null)
        {
            s_activeScene.Dispose();
        }


        // Call garbage collector 
        GC.Collect();

        s_activeScene = s_nextScene;

        s_nextScene = null;

        if(s_activeScene != null)
        {
            Console.Out.WriteLine("Initialize scene: " + (s_activeScene.GetType()));
            s_activeScene.Initialize();
        }
    }

    protected void ExitGame()
    {
        Exit();
    }
}
