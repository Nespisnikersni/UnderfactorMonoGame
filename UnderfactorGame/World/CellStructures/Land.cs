using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnderfactorGame.World.CellStructures;

public class Land : CellStructure
{
    public Texture2D Texture { get; set; }
    private int u;
    private int v;
    private int textureWidth;
    private int textureHeight;
    public Land(Texture2D texture,int u,int v,int textureWidth,int textureHeight) : base(texture)
    {
        Texture = texture;
        this.u = u;
        this.v = v;
        this.textureWidth = textureWidth;
        this.textureHeight = textureHeight;
    }
    public Land(Texture2D texture,int u,int v) : base(texture)
    {
        Texture = texture;
        this.u = u;
        this.v = v;
        this.textureWidth = 128;
        this.textureHeight = 128;
    }
    public Land(Texture2D texture) : base(texture)
    {
        Texture = texture;
        this.u = 0;
        this.v = 0;
        this.textureWidth = 128;
        this.textureHeight = 128;
    }

    public override void Draw(SpriteBatch spriteBatch,int x,int y,int cellSize)
    {
        spriteBatch.Draw(Texture, new Vector2(x * cellSize, y * cellSize),
            new Rectangle(u, v, textureWidth, textureHeight), Color.White);
    }

}