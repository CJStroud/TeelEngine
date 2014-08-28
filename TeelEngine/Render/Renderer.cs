using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Level;

namespace TeelEngine.Render
{
    public class Renderer
    {
        public Dictionary<string, SpriteSheet> SpriteSheets = new Dictionary<string, SpriteSheet>();
        public int GameTileSize { get; set; }

        public Renderer(List<SpriteSheet> spriteSheets, int tileSize)
        {
            SpriteSheets = spriteSheets.ToDictionary(d => d.Name);
            GameTileSize = tileSize;
        }

        public void Render(Level.Level level, SpriteBatch spriteBatch)
        {
            List<IRenderable> toRender = level.GetAllRenderables();

            foreach (var renderable in toRender)
            {
                ITexture texture = renderable.Texture;

                if (texture != null)
                {
                    SpriteSheet spriteSheet = SpriteSheets[texture.AssetName];
                    var screenPosition = new Point(
                                        (int)(renderable.Location.X * GameTileSize) - Camera.Lens.X + (int)(renderable.Offset.X * GameTileSize) + (GameTileSize / 2),
                                        (int)(renderable.Location.Y * GameTileSize) - Camera.Lens.Y + (int)(renderable.Offset.Y * GameTileSize) + (GameTileSize / 2));

                    texture.Render(spriteBatch, screenPosition, GameTileSize, spriteSheet, renderable.Rotation);
                }

            }
        }


    }
}
