using Microsoft.Xna.Framework.Graphics;
using UnderfactorGame.World.CellStructures;

namespace UnderfactorGame.Registry;

public static class Registries
{
    public static Registry<Texture2D> TextureRegistry { get; } = new(); 
    public static Registry<SpriteFont> FontRegistry { get; } = new(); 
    public static Registry<Land> LandRegistry { get; } = new(); 
    public static Registry<Building> BuildingRegistry { get; } = new();
    public static Registry<Effect> ShaderRegistry { get; } = new();
}