using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ITexture
    {
        string AssetName { get; set; }
        int TextureId { get; set; }

        void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet);
        void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet, float rotation);
    }
}
