using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.Animation;

namespace UnderfactorGame.World.CellStructures;

public class Building : CellStructure
{
    public Animation.Animation Animation;
    public Building(Texture2D texture) : base(texture)
    {
        Animation = new Animation.Animation(Enumerable.Range(0, 30).Select(i => new Frame(texture, i * 128)).ToList());
        Game1.Instance.TickSystem.SubscribeToTick("belt",() => Animation.TickUpdate());
    }

    public override void Draw(SpriteBatch spriteBatch, int x, int y, int cellSize)
    {
        Animation.Draw(spriteBatch, new Vector2(x * cellSize,y * cellSize));
    }
}