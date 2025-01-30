
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.Registry;
using UnderfactorGame.World.CellStructures;

namespace UnderfactorGame.World;

public class Cell
{
    public Land Land { get; set; }
    public Building Building { get; set; }
    public void Draw(SpriteBatch spriteBatch, int x, int y, int cellSize)
    {
        Land?.Draw(spriteBatch, x, y, cellSize);
        Building?.Draw(spriteBatch, x, y, cellSize);
    }
}