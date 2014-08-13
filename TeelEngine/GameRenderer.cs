using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using TeelEngine.Level;

namespace TeelEngine
{
    public class GameRenderer
    {
        public Texture2D SpriteSheet { get; set; }
        public int TileSize { get; set; }

        public void Render(Level.Level level, SpriteBatch spriteBatch)
        {
            foreach (var gameTile in level.GameTiles)
            {
                if (gameTile == null) continue;

                foreach (var subTile in gameTile.SubTiles)
                {

                    if (subTile is EntityTile)
                    {
                        var tile = subTile as EntityTile;
                            Texture2D texture = GetTextureAt(tile.TextureId);
                            var destinationRectangle = new Rectangle(tile.Location.X, tile.Location.Y, TileSize, TileSize);
                            spriteBatch.Draw(texture, destinationRectangle, Color.White);
                    }
                    else if (subTile is TerrainTile)
                    {
                        var tile = subTile as TerrainTile;
                        Texture2D texture = GetTextureAt(tile.TextureId);
                        var destinationRectangle = new Rectangle(tile.Location.X, tile.Location.Y, TileSize, TileSize);
                        spriteBatch.Draw(texture, destinationRectangle, Color.White);
                    }


                }
            }

        }

        private Texture2D GetTextureAt(int textureId)
        {
            // Todo: Get a tile sized section of the sprite sheet using the textureid to determine the position
            Texture2D texture = null;
            return texture;
        }

    }
}