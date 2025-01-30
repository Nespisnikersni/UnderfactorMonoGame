using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.Registry;

namespace UnderfactorGame.World.CellStructures;

public abstract class CellStructure
{
    protected Texture2D _texture;
    public Point Position;
    public bool IsStatic { get; set; } = false;
    public bool IsOpaque { get; set; } = true;
    public CellStructure(Texture2D texture)
    {
        _texture = texture;
    }
    public abstract void Draw(SpriteBatch spriteBatch,int x,int y,int cellSize);
    public void DrawSpirit(SpriteBatch spriteBatch, int x, int y, int cellSize, float alpha)
    {
        if (_texture == null)
        {
            return;
        }
        Color color = new Color(255, 255, 255, alpha); 
        spriteBatch.Draw(_texture,
            new Vector2(x * cellSize, y * cellSize),
            new Rectangle(0, 0, cellSize, cellSize),
            color);
    }
}