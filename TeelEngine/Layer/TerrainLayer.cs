using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class TerrainLayer : ILayer
    {
        public List<ITile> Tiles { get; set; } 

        public static List<ITile> TileList { get; set; }
        public int Priority { get; private set; }
        public Texture2D SpriteSheet { get; set; }

        public void Render(SpriteBatch spriteBatch)
        {
            
        }
    }
}
