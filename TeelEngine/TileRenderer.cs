using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Level;
using TeelEngine.Level.Interfaces;
using TeelEngine.Render;

namespace TeelEngine
{
    public class TileRenderer
    {
        public Dictionary<string, SpriteSheet> SpriteSheets = new Dictionary<string, SpriteSheet>();
        public int GameTileSize { get; set; }

        public TileRenderer(List<SpriteSheet> spriteSheets, int tileSize)
        {
            SpriteSheets = spriteSheets.ToDictionary(d => d.Name);
            GameTileSize = tileSize;
        }

        public void Render(Level.Level level, SpriteBatch spriteBatch)
        {
            foreach (var gameTile in level.GameTiles)
            {
                if (gameTile == null)
                {
                    continue;
                }

                foreach (var subTile in gameTile.SubTiles)
                {
                    if (Camera.IsWithinLens(GameTileSize, gameTile.Location))
                    {
                        if (subTile is IAnimatable)
                        {
                            var tile = subTile as IAnimatable;
                            if (tile.CurrentAnimation != null)
                            {
                                int row = tile.Animations[tile.CurrentAnimation];

                                var texture = tile.Texture as AnimatedTexture;
                                SpriteSheet spriteSheet = SpriteSheets[texture.AssetName];

                                if (texture != null)
                                {
                                    texture.Row = row;
                                    texture.Render(spriteBatch,
                                        new Point(gameTile.Location.X*GameTileSize, gameTile.Location.Y*GameTileSize), 
                                        GameTileSize, spriteSheet);
                                }
                            }
                        }
                        else if (subTile is IRenderable)
                        {
                            var tile = subTile as IRenderable;
                            SpriteTexture texture = tile.Texture as SpriteTexture;

                            if (texture != null)
                            {
                                SpriteSheet spriteSheet = SpriteSheets[texture.AssetName];

                                var offset = new Vector2(0,0);

                                var moveable = subTile as IMoveable;
                                
                                if(moveable != null)offset = moveable.Offset;

                                var screenPosition = new Point(
                                    (gameTile.Location.X*GameTileSize) - Camera.Lens.X + (int)(offset.X * GameTileSize),
                                    (gameTile.Location.Y*GameTileSize) - Camera.Lens.Y + (int)(offset.Y * GameTileSize));

                                texture.Render(spriteBatch, screenPosition, spriteSheet, GameTileSize);
                            }

                        }
                    }
                }
            }
        }
    }
}