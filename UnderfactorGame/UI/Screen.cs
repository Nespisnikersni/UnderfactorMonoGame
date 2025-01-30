using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnderfactorGame.UI;

public abstract class Screen
{
    private List<Widget> elements = new();
    
    protected void AddElement(Widget element)
    {
        elements.Add(element);
    }

    protected void RemoveElement(Widget element)
    {
        elements.Remove(element);
    }
    public virtual void Initialize(){}
    public void Update(GameTime gameTime)
    {
        foreach (var element in elements)
        {
            element.Update(gameTime);
        }
        HandleKeyboardInput();
        HandleMouseInput();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        foreach (var element in elements)
        {
            element.Draw(spriteBatch);
        }
        spriteBatch.End();
    }
    private void HandleKeyboardInput()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        foreach (var key in keyboardState.GetPressedKeys())
        {
            List<ModifierKeys> modifiers = new List<ModifierKeys>();

            if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
            {
                modifiers.Add(ModifierKeys.Shift);
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
            {
                modifiers.Add(ModifierKeys.Ctrl);
            }

            if (keyboardState.IsKeyDown(Keys.LeftAlt) || keyboardState.IsKeyDown(Keys.RightAlt))
            {
                modifiers.Add(ModifierKeys.Alt);
            }

            foreach (var element in elements)
            {
                element.OnKeyPressed(key, modifiers);       
            }
        }
    }
    private void HandleMouseInput()
    {
        MouseState mouseState = Mouse.GetState();
        Point mousePosition = new Point(mouseState.X, mouseState.Y);

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            foreach (var element in elements)
            {
                element.OnMouseClicked(mousePosition, 0); 
            }
        }

        if (mouseState.RightButton == ButtonState.Pressed)
        {
            foreach (var element in elements)
            {
                element.OnMouseClicked(mousePosition, 1); 
            }
        }
    }

    public void Close()
    {
        Game1.Instance.SetScreen(null);
    }
}
public enum ModifierKeys
{
    None,
    Shift,
    Ctrl,
    Alt
}