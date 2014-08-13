using System.Configuration;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

namespace TeelEngine.Level
{
    public class Level
    {
        public Level(Size mapSize)
        {
            GameTiles = new GameTile[mapSize.Width, mapSize.Height];
        }

        public Level(int width, int height)
        {
            GameTiles = new GameTile[width, height];
        }

        public GameTile[,] GameTiles { get; private set; }

        public void AddTile(ITile tile, Point location)
        {
           // GameTile gTile = GameTiles[location.X, location.Y];
            if (GameTiles[location.X, location.Y] == null) GameTiles[location.X, location.Y] = new GameTile {Location = location};

            GameTiles[location.X, location.Y].SubTiles.Add(tile);
        }
    }
}