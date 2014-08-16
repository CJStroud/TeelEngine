using System.Collections.Generic;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

namespace TeelEngine.Level
{
    public class Level
    {
        public Level(Size mapSize)
        {
            GameTiles = new GameTile[mapSize.Width, mapSize.Height];
            Entities = new List<Entity>();
        }

        public Level(int width, int height)
        {
            GameTiles = new GameTile[width, height];
            Entities = new List<Entity>();
        }

        public GameTile[,] GameTiles { get; private set; }
        public List<Entity> Entities { get; set; }

        public void AddTile(ITile tile, Point location)
        {
            if (location.X < GameTiles.GetUpperBound(0) && location.Y < GameTiles.GetUpperBound(1))
            {
                if (GameTiles[location.X, location.Y] == null)
                    GameTiles[location.X, location.Y] = new GameTile {Location = location};

                GameTiles[location.X, location.Y].SubTiles.Add(tile);
            }
        }
    }
}