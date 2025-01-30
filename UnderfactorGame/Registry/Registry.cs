using System;
using System.Collections.Generic;

namespace UnderfactorGame.Registry;

public class Registry<TValue>
{
    private readonly Dictionary<string, TValue> _items;

    public Registry()
    {
        _items = new Dictionary<string, TValue>();
    }

    public void Add(string key, TValue value)
    {
        if (_items.ContainsKey(key))
        {
            throw new Exception($"Key '{key}' already exists in the registry.");
        }
        _items[key] = value;
    }

    public TValue Get(string key)
    {
        return _items.ContainsKey(key) ? _items[key] : default;
    }

    public bool ContainsItem(string key)
    {
        return _items.ContainsKey(key);
    }

    public void RemoveItem(string key)
    {
        if (_items.ContainsKey(key))
            _items.Remove(key);
    }

    public IEnumerable<string> GetAllKeys()
    {
        return _items.Keys;
    }

    public IEnumerable<TValue> GetAllValues()
    {
        return _items.Values;
    }
}