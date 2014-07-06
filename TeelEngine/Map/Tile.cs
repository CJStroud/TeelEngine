using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class Tile : ITile
    {
        public Vector2 Location { get; set; }
        public string TextureName { get; set; }
        public int Index { get; set; }

        [ContentSerializerIgnore]
        public Vector2 ScreenPosition { get; set; }

        public void Render(SpriteBatch spriteBatch)
        {
            Render(spriteBatch, ScreenPosition);
        }

        public void Render(SpriteBatch spriteBatch, Vector2 screenLocation)
        {
            ITexture texture = Globals.TextureController.Get(TextureName);
            var spriteTexture = (SpriteTexture)texture;
            if (spriteTexture != null)
            {
                spriteTexture.Render(spriteBatch, Index, ScreenPosition);
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Initialize()
        {
            ScreenPosition = new Vector2(Location.X * Globals.TileSize, Location.Y * Globals.TileSize);
        }
    }
}
