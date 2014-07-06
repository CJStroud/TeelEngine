using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class TerrainLayer : ILayer
    {
        public List<ITile> Tiles { get; set; } 

        public static List<ITile> TileList { get; set; }

        public TerrainLayer()
        {
            TileList = new List<ITile>();
            Tiles = new List<ITile>();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            Camera.Render(spriteBatch, Tiles);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Initialize()
        {
            foreach (var tile in Tiles)
            {
                tile.Initialize();
            }
        }

        public void Add(ITile tile)
        {
            Tiles.Add(tile);
        }
    }
}
