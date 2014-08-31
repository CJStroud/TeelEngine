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

        public virtual void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet)
        {
            Render(spriteBatch, screenPos, gameTileSize, spriteSheet, 0F);
        }

        public virtual void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet, float rotation)
        {
            var sourceRectangle = spriteSheet.GetTileRectangle(TextureId);

            var destinationRectangle = new Rectangle(screenPos.X, screenPos.Y, gameTileSize,
                gameTileSize);

            spriteBatch.Draw(spriteSheet.Texture, destinationRectangle, sourceRectangle, Color.White, rotation, new Vector2(spriteSheet.TileSize / 2F, spriteSheet.TileSize / 2F), SpriteEffects.None, 0);
        }

        public SpriteTexture(string assetName, int textureId)
        {
            AssetName = assetName;
            TextureId = textureId;
        }
    }
}
