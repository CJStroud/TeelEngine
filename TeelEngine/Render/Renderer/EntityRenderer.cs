using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Render;

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
                SpriteSheet spriteSheet = SpriteSheets[entity.Texture.AssetName];

                var screenPosition = new Point(
                                    (int)(entity.Location.X * GameTileSize) - Camera.Lens.X + (int)(entity.Offset.X * GameTileSize) + (GameTileSize / 2),
                                    (int)(entity.Location.Y * GameTileSize) - Camera.Lens.Y + (int)(entity.Offset.Y * GameTileSize) + (GameTileSize / 2));

                var animatedEntity = entity as IAnimatable;

                if (animatedEntity != null)
                {
                    var animatedTexture = animatedEntity.Texture as AnimatedTexture;

                    if (animatedEntity.CurrentAnimation != null)
                    {
                        
                        animatedTexture.Row = animatedEntity.Animations[animatedEntity.CurrentAnimation];
                        animatedTexture.Paused = false;
                    }
                    else
                    {
                        animatedTexture.Paused = true;
                    }
                }

                if (entity.Rotation > 0 || entity.Rotation < 0)
                {
                    entity.Texture.Render(spriteBatch, screenPosition, GameTileSize, spriteSheet, entity.Rotation);
                    return;
                }
                entity.Texture.Render(spriteBatch, screenPosition, GameTileSize, spriteSheet);
            }
           
        }

    }
}
