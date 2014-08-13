using System.Collections.Generic;

namespace TeelEngine.Level
{
    public class GameTile
    {
        public GameTile()
        {
            SubTiles = new List<ITile>();
        }

        public List<ITile> SubTiles { get; private set; }

        public void AddTile(ITile tile)
        {
            SubTiles.Add(tile);
        }

    }
}