using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace UnderfactorGame.System;

public class TickSystem
{
    public int CurrentTick { get; private set; }
    public int Speed { get; private set; }
    public float TickInterval { get; private set; }

    private Dictionary<string, Action> _tickSubscribers = new();

    private float _timeSinceLastTick; 

    public TickSystem()
    {
        SetSpeed(60);
    }

    public void SubscribeToTick(string name, Action action)
    {
        _tickSubscribers[name] = action;
    }

    public void UnsubscribeFromTick(string name)
    {
        if (_tickSubscribers.ContainsKey(name))
        {
            _tickSubscribers.Remove(name);
        }
    }

    public void SetSpeed(int speed)
    {
        Speed = speed;
        TickInterval = speed == 0 ? 0 : 1 / (float)speed;
    }

    public void Update(GameTime gameTime)
    {
        if (TickInterval == 0) return;

        _timeSinceLastTick += (float)gameTime.ElapsedGameTime.TotalSeconds;

        while (_timeSinceLastTick >= TickInterval)
        {
            CurrentTick++;
            InvokeTickEvents();
            _timeSinceLastTick -= TickInterval; 
        }
    }

    private void InvokeTickEvents()
    {
        foreach (var subscriber in _tickSubscribers)
        {
            subscriber.Value.Invoke();
        }
    }
}