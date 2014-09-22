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

        public void Render(List<IRenderable> renderables , SpriteBatch spriteBatch)
        {

            foreach (var renderable in renderables)
            {
                if (
                    Camera.Lens.Intersects(new Rectangle((int)renderable.Location.X * GameTileSize, (int)renderable.Location.Y * GameTileSize, GameTileSize,
                        GameTileSize)))
                {

                    SpriteTexture texture = renderable.Texture;

                    if (texture != null)
                    {
                        SpriteSheet spriteSheet = SpriteSheets[texture.AssetName];
                        var screenPosition = new Point(
                            (int) (renderable.Location.X*GameTileSize) - Camera.Lens.X +
                            (int) (renderable.Offset.X*GameTileSize) + (GameTileSize/2),
                            (int) (renderable.Location.Y*GameTileSize) - Camera.Lens.Y +
                            (int) (renderable.Offset.Y*GameTileSize) + (GameTileSize/2));

                        texture.Render(spriteBatch, screenPosition, GameTileSize, spriteSheet, renderable.Rotation);
                    }
                }

            }

            Texture2D textureCollision = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            textureCollision.SetData<Color>(new Color[] { new Color(255, 0, 0, 125) });
            foreach (var collision in CollisionDetection.Collisions)
            {
                spriteBatch.Draw(textureCollision, new Rectangle(collision.X * GameTileSize - Camera.Lens.X, collision.Y * GameTileSize - Camera.Lens.Y, GameTileSize, GameTileSize), Color.White);
            }
        }


    }
}
