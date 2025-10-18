using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace WizardsVsWirebacks.Scenes;

// TODO: Decide if we want to have this as an asset bank, or just use the existing texture atlas and pass that around
// Currently it's only partially implemented, used for the background and 
public class LevelAssets //: IDisposable
{
    private TextureAtlas _atlas;

    // ! Review interactions with scaling/transforms, waypoints -> change to textureRegion or something?

    private AnimatedSprite _clankaSpriteWalkUp;
    private AnimatedSprite _clankaSpriteWalkDown;
    private AnimatedSprite _clankaSpriteWalkLeft;
    private AnimatedSprite _clankaSpriteWalkRight;
    
    private Texture2D _background;
    
    private Sprite _chainsawSprite;
    
    public Dictionary<string, Texture2D> RawTextures { get; private set; } = new() ;
    public Dictionary<string, TextureRegion> Regions { get; private set; } = new();
    public Dictionary<string, Sprite> Sprites { get; private set; } = new();
    public Dictionary<string, AnimatedSprite> AnimatedSprites { get; private set; } = new();


    public LevelAssets( )
    {
        Initialize();
    }

    private void Initialize()
    {
    }
    
    public void LoadContent()
    {
        _atlas = TextureAtlas.FromFile(Core.Content, "images/objectAtlas-definition.xml");
        _background = Core.Content.Load<Texture2D>("Tilemaps/City/simplified/Level_1/_bg");
        RawTextures.Add("background", _background);
        LoadEnemyAssets();
        LoadTowerAssets();
    }

    public void LoadEnemyAssets()
    {
        _clankaSpriteWalkDown = _atlas.CreateAnimatedSprite("clanka-walk-down");
        AnimatedSprites.Add("clanka-walk-down", _clankaSpriteWalkDown);
        _chainsawSprite = _atlas.CreateSprite("chainsawmancer-1");
        Sprites.Add("chainsawmancer", _chainsawSprite);
        //_clankaSprite.Origin = new Vector2(_clankaSprite.Width * 0.25f, _clankaSprite.Height * 0.25f); // Centers it in a 16px tile!
    }
    
    public void LoadTowerAssets()
    {
        
    }
}