using System;
using  System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace UnderfactorGame.Animation;

public class Animation
{
    private List<Frame> _frames;   
    private int _currentFrameIndex;   

    public bool IsLooping { get; set; }  

    public Animation(List<Frame> frames)
    {
        _frames = frames;
        _currentFrameIndex = 0;
        IsLooping = true;  
    }

    public void TickUpdate()
    {
        if (_currentFrameIndex < _frames.Count - 1)
        {
            _currentFrameIndex++;
        }
        else
        {
            Reset();
        }
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        _frames[_currentFrameIndex].Draw(spriteBatch, position);
    }

    public void Reset()
    {
        _currentFrameIndex = 0;
    }

    public static Animation FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Animation>(json);
    }

    public static Animation FromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return FromJson(json);
    }
}