using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;
using WizardsVsWirebacks.GameObjects;
using WizardsVsWirebacks.Scenes.City;
using WizardsVsWirebacks.Screens;

namespace WizardsVsWirebacks.Scenes;

public class LevelScene : Scene
{


	private Level _level;
    private LevelScreen _ui;
    private LevelAssets _assets;
    
    private int _currency;

    
    private void InitializeUi()
    {
        GumService.Default.Root.Children.Clear();
        _ui = new LevelScreen();
        _ui.TowerIconPushed += HandleTowerSelected;
        _ui.AddToRoot();
    }
    
    public override void Initialize()
    {
        InitializeUi();
        _assets = new LevelAssets();
        _level = new Level();
        base.Initialize();
    }

    public override void LoadContent()
    {
        _assets.LoadContent();
    }
    
    public void HandleTowerSelected(object sender, TowerSelectedEventArgs e)
    {
        _level.SelectedTower = e.TowerType;
    }
    public override void Update(GameTime gameTime)
    {
        if (GameController.Exit()) // Change to input handle
        {
            Core.ChangeScene(new CityScene());
        }
        _level.Update(gameTime);
        GumService.Default.Update(gameTime);
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
        Core.GraphicsDevice.Viewport = Core.Viewport; // * Work in progress, not even sure if this does anything (it doesn't)
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: LevelInputManager.GetTransform());

        Core.SpriteBatch.Draw(_assets.RawTextures["background"], Vector2.Zero, Color.White);
        _level.Draw(gameTime);
        
        Core.SpriteBatch.End(); 
        
        GumService.Default.Draw();
        base.Draw(gameTime);
    }
    
}
