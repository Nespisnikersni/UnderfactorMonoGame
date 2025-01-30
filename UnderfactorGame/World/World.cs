using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.Control;
using UnderfactorGame.Registry;
using UnderfactorGame.World.CellStructures;
using Color = Microsoft.Xna.Framework.Color;
using Point = System.Drawing.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace UnderfactorGame.World;

public class World
{
    private Cell[,] grid;
    private int width, height; 
    public readonly Cursor Cursor;
    public readonly int cellSize = 128; 
    public List<Point> updateCells = new();
    private Camera camera = Game1.Instance.Camera;

    
    public World(int width, int height)
    {
        this.width = width;
        this.height = height;
        Cursor = new Cursor(this,new Point(70, 70));
        grid = new Cell[width, height];
    }

    public Cell GetOrCreateCell(int worldX, int worldY)
    {
        grid[worldX, worldY] ??= new Cell();
        return grid[worldX, worldY];
    }
    public Cell GetCell(int worldX, int worldY)
    {
        return grid[worldX, worldY];
    }
    public Cell GetCell(Point point)
    {
        return GetCell(point.X, point.Y);
    }

    public void Update(GameTime gameTime)
    {
        Cursor.Update(gameTime);
    }

    private static readonly Rectangle CellRectangle = new(0,0,128,128);
    private static readonly Texture2D CellTexture = Registries.TextureRegistry.Get("cell");
    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
    {
        spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(),blendState: BlendState.AlphaBlend,sortMode: SpriteSortMode.Texture,
            samplerState: SamplerState.LinearClamp,depthStencilState: DepthStencilState.Default,rasterizerState: RasterizerState.CullNone);
        RectangleF viewArea = camera.GetViewArea(graphics);
        
        int startX = Math.Max(0, (int)(viewArea.X / cellSize));
        int endX = Math.Min(width, (int)((viewArea.X + viewArea.Width) / cellSize));
        int startY = Math.Max(0, (int)(viewArea.Y / cellSize));
        int endY = Math.Min(height, (int)((viewArea.Y + viewArea.Height) / cellSize));
        
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                spriteBatch.Draw(CellTexture,
                    new Vector2(x * cellSize, y * cellSize), CellRectangle, Color.White);
                grid[x,y]?.Draw(spriteBatch, x, y, cellSize);
            }
        }
        Cursor.Draw(spriteBatch);
        spriteBatch.End();
    }
}