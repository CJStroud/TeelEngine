using System.Drawing;

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
    }
}