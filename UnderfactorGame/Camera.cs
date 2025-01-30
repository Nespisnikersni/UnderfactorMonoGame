using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnderfactorGame;

public class Camera
{
    private Vector2 _position; 
    private float _zoom;     
    private float _rotation;  

    public Camera()
    {
        _position = Vector2.Zero;
        _zoom = 1f;
        _rotation = 0f;
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(-_position.X, -_position.Y, 0) *
               Matrix.CreateRotationZ(_rotation) *
               Matrix.CreateScale(_zoom);
    }
    public RectangleF GetViewArea(GraphicsDevice graphicsDevice)
    {
        float viewportWidth = graphicsDevice.Viewport.Width;
        float viewportHeight = graphicsDevice.Viewport.Height;

        float scaledWidth = viewportWidth / _zoom;
        float scaledHeight = viewportHeight / _zoom;

        Vector2 topLeft = _position;

        return new RectangleF(topLeft.X, topLeft.Y,scaledWidth, scaledHeight);
    }
    public void MoveCamera(GameTime gameTime, GraphicsDevice graphicsDevice, World.World _currentWorld)
    {
        if (_currentWorld != null)
        {
            int scaledWidth = (int)(graphicsDevice.Viewport.Width / _zoom);
            int scaledHeight = (int)(graphicsDevice.Viewport.Height / _zoom);

            Vector2 targetCameraPosition = new Vector2(
                (_currentWorld.Cursor.X * _currentWorld.cellSize + _currentWorld.cellSize / 2) - scaledWidth / 2,
                (_currentWorld.Cursor.Y * _currentWorld.cellSize + _currentWorld.cellSize / 2) - scaledHeight / 2
            );

            float cameraSpeed = 5f; 
            Position = Vector2.Lerp(
                Position,
                targetCameraPosition,
                cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds
            );
        }
    }
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public float Zoom
    {
        get => _zoom;
        set => _zoom = MathHelper.Clamp(value, 0.1f, 10f);
    }

    public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }
}