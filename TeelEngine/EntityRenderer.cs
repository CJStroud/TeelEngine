using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class EntityRenderer
    {
        public Dictionary<string, SpriteSheet> SpriteSheets = new Dictionary<string, SpriteSheet>();
        public int GameTileSize { get; set; }

        public EntityRenderer(List<SpriteSheet> spriteSheets, int tileSize)
        {
            SpriteSheets = spriteSheets.ToDictionary(d => d.Name);
            GameTileSize = tileSize;
        }

        public void Render(Level.Level level, SpriteBatch spriteBatch)
        {
            foreach (var entity in level.Entities)
            {
                
            }
           
        }

    }
}
