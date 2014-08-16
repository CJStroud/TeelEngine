using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Render;

namespace TeelEngine 
{
    public class SpriteTexture : ITexture
    {
        public string AssetName { get; set; }
        public int TextureId { get; set; }

        public SpriteTexture(string assetName, int textureId)
        {
            AssetName = assetName;
            TextureId = textureId;
        }

        public void Render(SpriteBatch spriteBatch, Point screenPos, SpriteSheet spriteSheet, int gameTileSize)
        {
            var sourceRectangle = spriteSheet.GetTileRectangle(TextureId);

            var destinationRectangle = new Rectangle(screenPos.X, screenPos.Y, gameTileSize,
                gameTileSize);

            spriteBatch.Draw(spriteSheet.Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
