using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnderfactorGame.Registry;
using UnderfactorGame.World.CellStructures;
using Point = System.Drawing.Point;

namespace UnderfactorGame.Control;

public class Cursor
{
    private Point _position;
    private float _moveCooldown; 
    private const float MoveDelay = 0.2f;
    private CellStructure _cellStructure;
    private World.World _world;

    public Cursor(World.World world,Point startPosition)
    {
        _world = world;
        _position = startPosition;
        _moveCooldown = 0;
    }

    public void Update(GameTime gameTime)
    {
        _moveCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_moveCooldown > 0)
            return;

        bool moveUp = Game1.KeyBindingManager.IsKeyPressed("moveUp");
        bool moveDown = Game1.KeyBindingManager.IsKeyPressed("moveDown");
        bool moveLeft = Game1.KeyBindingManager.IsKeyPressed("moveLeft");
        bool moveRight = Game1.KeyBindingManager.IsKeyPressed("moveRight");
        
        if (moveUp)
        {
            Move(0, -1);
        }
        if (moveDown)
        {
            Move(0, 1);
        }
        if (moveLeft)
        {
            Move(-1, 0);
        }
        if (moveRight)
        {
            Move(1, 0);
        }

        if (moveUp || moveDown || moveLeft || moveRight)
        {
            _moveCooldown = MoveDelay;
        }

        if (Game1.KeyBindingManager.IsKeyPressed("setBelt"))
        {
            _cellStructure = Registries.BuildingRegistry.Get("belt");
        }
        if (Game1.KeyBindingManager.IsKeyPressed("build"))
        {
            Build();
            _world.updateCells.Add(new Point(X,Y));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Registries.TextureRegistry.Get("cursor"),
        new Vector2(X * 128, Y * 128), new Rectangle(0, 0, 128, 128), Color.White);
        _cellStructure?.DrawSpirit(spriteBatch,X,Y,128,0.01f);
    }

    public void Build()
    {
        if (_cellStructure == null)
        {
            return;
        }

        if (_cellStructure is Building building && _world.GetCell(X,Y).Building == null)
        {
            _world.GetCell(X,Y).Building = building;
        }
        if (_cellStructure is Land land && _world.GetCell(X,Y).Land == null)
        {
            _world.GetCell(X,Y).Land = land;
        }
    }

    private void Move(int deltaX, int deltaY)
    {
        _position.X += deltaX;
        _position.Y += deltaY;
    }

    public int X => _position.X;
    public int Y => _position.Y;

    public Point Position => _position;
    public CellStructure GetCellStructure() => _cellStructure;
    public void SetCellStructure(CellStructure structure) => _cellStructure = structure;
}