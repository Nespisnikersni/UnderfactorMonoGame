using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.Registry;

namespace UnderfactorGame.Util;

using Microsoft.Xna.Framework;

public class FpsCounter
{
    private int frameCount = 0;
    private float elapsedTime = 0f; 
    private int fps = 0; 

    public void Update(GameTime gameTime)
    {
        frameCount++;
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime >= 1f)
        {
            fps = frameCount; 
            frameCount = 0; 
            elapsedTime = 0f; 
        }
    }

    public int GetFps()
    {
        return fps;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        string fpsText = $"FPS: {fps}";
        spriteBatch.DrawString(Registries.FontRegistry.Get("default"), fpsText, Vector2.Zero, Color.White);
        spriteBatch.End();
    }
}