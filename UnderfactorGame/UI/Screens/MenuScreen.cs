using Microsoft.Xna.Framework;
using UnderfactorGame.Registry;
using UnderfactorGame.World;
using UnderfactorGame.World.CellStructures;
using Point = System.Drawing.Point;

namespace UnderfactorGame.UI.Screens;

public class MenuScreen : Screen
{
    public override void Initialize()
    {
        Button button = new(1920 / 2,1080 / 2,100,40,"Play");
        button.SetOnClickAction(() =>
        {
            Game1.Instance.SetWorld(new World.World(10000,10000));
            World.World world = Game1.Instance.GetWorld();
            for (int x = 0; x < 45; x++)
            {
                for (int y = 0; y < 45; y++)
                {
                    Building belt = Registries.BuildingRegistry.Get("belt");
                    world.updateCells.Add(new Point(x,y));
                    world.GetOrCreateCell(x, y).Building = belt;
                }
            }
            Close();
        });
        AddElement(button);
    }
}