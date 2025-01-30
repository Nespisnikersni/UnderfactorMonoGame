using System.Collections.Generic;

namespace UnderfactorGame.Control;

using Microsoft.Xna.Framework.Input;

public class KeyBindingManager
{
    private Dictionary<string, Keys> _bindings;

    public KeyBindingManager()
    {
        _bindings = new Dictionary<string, Keys>();
    }
    
    public void SetBinding(string action, Keys key)
    {
        if (_bindings.ContainsKey(action))
        {
            _bindings[action] = key;  
        }
        else
        {
            _bindings.Add(action, key);  
        }
    }

    public Keys GetBinding(string action)
    {
        if (_bindings.ContainsKey(action))
        {
            return _bindings[action];
        }
        else
        {
            throw new KeyNotFoundException($"No key binding found for action: {action}");
        }
    }

    public bool IsKeyPressed(string action)
    {
        if (_bindings.ContainsKey(action))
        {
            KeyboardState keyboardState = Keyboard.GetState();
            return keyboardState.IsKeyDown(_bindings[action]);
        }
        else
        {
            throw new KeyNotFoundException($"No key binding found for action: {action}");
        }
    }

    public void RemoveBinding(string action)
    {
        if (_bindings.ContainsKey(action))
        {
            _bindings.Remove(action);
        }
        else
        {
            throw new KeyNotFoundException($"No key binding found for action: {action}");
        }
    }

    public void ClearBindings()
    {
        _bindings.Clear();
    }
}