using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnderfactorGame.UI;

public abstract class Widget
{
    public Vector2 Position { get; set; }
    protected int _width;
    protected int _height;

    public Widget(int x, int y, int width, int height)
    {
        Position = new Vector2(x, y);
        _width = width;
        _height = height;
    }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public virtual void OnKeyPressed(Keys key, List<ModifierKeys> modifiers){}
    
    public virtual void OnMouseClicked(Point mousePosition, int button){}

    public bool IsMouseOver(Point mousePosition)
    {
         return mousePosition.X >= Position.X && mousePosition.X <= Position.X + _width &&
           mousePosition.Y >= Position.Y && mousePosition.Y <= Position.Y + _height;       
    }
}