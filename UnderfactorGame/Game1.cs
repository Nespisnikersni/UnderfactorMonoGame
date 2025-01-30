using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using UnderfactorGame.Control;
using UnderfactorGame.Registry;
using UnderfactorGame.System;
using UnderfactorGame.UI;
using UnderfactorGame.UI.Screens;
using UnderfactorGame.Util;
using UnderfactorGame.World.CellStructures;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace UnderfactorGame;

public class Game1 : Game
{
    public static Game1 Instance { get; private set; } = new();
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World.World _currentWorld;
    public static readonly KeyBindingManager KeyBindingManager = new();
    public readonly Camera Camera = new();
    private Screen _currentScreen;
    private FpsCounter _fpsCounter = new();
    public TickSystem TickSystem = new();
    public Game1()
    {
        Instance ??= this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = false;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Registries.TextureRegistry.Add("cell", Content.Load<Texture2D>("textures/WorldSheet"));
        Registries.TextureRegistry.Add("cursor", Content.Load<Texture2D>("textures/Cursor"));
        Registries.TextureRegistry.Add("belt", Content.Load<Texture2D>("textures/BeltSheet"));
        Registries.BuildingRegistry.Add("belt", new Building(Registries.TextureRegistry.Get("belt")));
        Texture2D buttonTexture = new Texture2D(GraphicsDevice, 1, 1);
        buttonTexture.SetData([Color.Green]);
        Registries.TextureRegistry.Add("button", buttonTexture);
        Registries.FontRegistry.Add("default", Content.Load<SpriteFont>("font/DefaultFont"));
        Registries.LandRegistry.Add("ground", new Land(null));
        KeyBindingManager.SetBinding("zoomm",Keys.Down);
        KeyBindingManager.SetBinding("zoomp",Keys.Up);
        KeyBindingManager.SetBinding("setBelt",Keys.NumPad0);
        KeyBindingManager.SetBinding("build",Keys.K);
        KeyBindingManager.SetBinding("moveUp",Keys.W); 
        KeyBindingManager.SetBinding("moveDown",Keys.S); 
        KeyBindingManager.SetBinding("moveLeft",Keys.A); 
        KeyBindingManager.SetBinding("moveRight",Keys.D); 
        TickSystem.SubscribeToTick("zoom",() => 
        {
            if(KeyBindingManager.IsKeyPressed("zoomp"))
            {
                Camera.Zoom += 0.01f;
            }
            if(KeyBindingManager.IsKeyPressed("zoomm"))
            {
                Camera.Zoom -= 0.01f;
            }
        });
        SetScreen(new MenuScreen());
    }

    protected override void Update(GameTime gameTime)
    {
        _fpsCounter.Update(gameTime);
        TickSystem.Update(gameTime);
        Camera.MoveCamera(gameTime,GraphicsDevice,_currentWorld);
        _currentWorld?.Update(gameTime);
        _currentScreen?.Update(gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _currentWorld?.Draw(_spriteBatch,GraphicsDevice);
        _fpsCounter.Draw(_spriteBatch);
        _currentScreen?.Draw(_spriteBatch);
        base.Draw(gameTime);
    }

    public void SetScreen(Screen screen)
    {
        _currentScreen = screen;
        _currentScreen?.Initialize();
    }

    public void SetWorld(World.World world)
    {
        _currentWorld = world;
    }

    public World.World GetWorld()
    {
        return _currentWorld;
    }
    public GraphicsDevice GraphicsDevice => _graphics.GraphicsDevice;
}