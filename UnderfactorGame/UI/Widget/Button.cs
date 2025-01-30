using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnderfactorGame.Registry;

namespace UnderfactorGame.UI;

public class Button : Widget
{
    public Button(int x, int y, int width, int height, string text) : base(x, y, width, height)
    {
        Text = text;
    }

    public Texture2D Texture { get; set; } = Registries.TextureRegistry.Get("button");
    public string Text { get; set; }
    public Action OnClick { get; set; }
    
    public override void Update(GameTime gameTime)
    {
    }

    public override void OnMouseClicked(Point mousePosition, int button)
    {
        if (IsMouseOver(mousePosition) && OnClick != null)
        {
            OnClick();
        }
        base.OnMouseClicked(mousePosition, button);
    }

    public void SetOnClickAction(Action action)
    {
        OnClick = action;
    }
    public override void OnKeyPressed(Keys key, List<ModifierKeys> modifierKeysList)
    {
        base.OnKeyPressed(key, modifierKeysList);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, new Rectangle(Position.ToPoint(),new Point(_width,_height)), Color.White);
        spriteBatch.DrawString(
            Registries.FontRegistry.Get("default"),
            Text,
            Position + new Vector2(10, 10),
            Color.White
        );
    }

}