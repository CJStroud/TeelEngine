using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class GameTile
    {
        public GameTile()
        {
            SubTiles = new List<ITile>();
        }

        public List<ITile> SubTiles { get; private set; }
        public Point Location { get; set; }

        public void AddTile(ITile tile)
        {
            SubTiles.Add(tile);
        }

        public void Update(Level level, GameTime gameTime)
        {
            foreach (var subTile in SubTiles)
            {
                subTile.Update(level, gameTime);
            }
        }
    }
}