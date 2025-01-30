using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnderfactorGame.Animation;

public class Frame
{
    public Texture2D Texture { get; private set; } 
    public Rectangle SourceRectangle { get; private set; } 

    public Frame(Texture2D texture, int u = 0, int v = 0, int width = 128, int height = 128)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(u, v, width, height); 
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(Texture, position, SourceRectangle, Color.White); 
    }
}