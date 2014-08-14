using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Level;
using TeelEngine.Render;

namespace TeelEngine
{
    public class TileRenderer
    {
        public Dictionary<string, Texture2D> SpriteSheets = new Dictionary<string, Texture2D>();
        public int TileSize { get; set; }

        public TileRenderer(List<Texture2D> spriteSheets, int tileSize)
        {
            SpriteSheets = spriteSheets.ToDictionary(d => d.Name);
            TileSize = tileSize;
        }

        public void Render(Level.Level level, SpriteBatch spriteBatch)
        {
            int gameTileSize = TileSize*2;

            foreach (var gameTile in level.GameTiles)
            {
                if (gameTile == null)
                {
                    continue;
                }

                foreach (var subTile in gameTile.SubTiles)
                {
                    if (Camera.IsWithinLens(gameTileSize, gameTile.Location))
                    {
                        if (subTile is IAnimatable)
                        {
                            var tile = subTile as IAnimatable;
                            Texture2D spritesheet = SpriteSheets[tile.AssetName];
                            if (tile.CurrentAnimation != null)
                            {
                                int row = tile.Animations[tile.CurrentAnimation];

                                AnimatedTexture texture = new AnimatedTexture(new Vector2(0), 4, 4, 4, false);
                                texture.Texture = spritesheet;
                                texture.Row = row;
                                texture.Render(spriteBatch,
                                    new Vector2(gameTile.Location.X*TileSize, gameTile.Location.Y*TileSize), TileSize);
                            }
                        }
                        else if (subTile is IRenderable)
                        {
                            var tile = subTile as IRenderable;
                            int id = tile.TextureId;
                            Texture2D spriteSheet = SpriteSheets[tile.AssetName];

                            int spriteSheetTileWidth = spriteSheet.Width/TileSize;

                            int xPixelPos = (id%spriteSheetTileWidth)*TileSize;
                            int yPixelPos = (id/spriteSheetTileWidth)*TileSize;


                            var sourceRectangle = new Rectangle(xPixelPos, yPixelPos, TileSize, TileSize);
                            var destRectangle = new Rectangle((gameTile.Location.X*gameTileSize) - Camera.Lens.X,
                                (gameTile.Location.Y*gameTileSize) - Camera.Lens.Y,
                                gameTileSize, gameTileSize);

                            spriteBatch.Draw(spriteSheet, destRectangle, sourceRectangle, Color.White);

                        }
                    }
                }
            }
        }
    }
}