using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public static class Camera
    {
        public static Rectangle Lens { get; set; }
        public static void Render(SpriteBatch spriteBatch, IEnumerable<ISprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                var spriteRectangle = ConvertLocationFromGrid(sprite.Location);
                var lens = Lens;
                lens.Width += (Globals.TileSize*2);
                lens.Height += (Globals.TileSize * 2);
                lens.X -= Globals.TileSize;
                lens.Y -= Globals.TileSize;
                if (lens.Intersects(spriteRectangle))
                {
                    var spriteScreenLocation = sprite.ScreenPosition;
                    spriteScreenLocation.X -= Lens.X;
                    spriteScreenLocation.Y -= Lens.Y;

                    sprite.Render(spriteBatch, spriteScreenLocation);
                }
            }
        }
        private static Rectangle ConvertLocationFromGrid(Vector2 location)
        {
            return new Rectangle(
                (int)(location.X * Globals.TileSize), 
                (int)(location.Y * Globals.TileSize), 
                Globals.TileSize, 
                Globals.TileSize);
        }

        public static void UpdateLensPosition(Point newStartPosition)
        {
            Camera.Lens = new Rectangle(newStartPosition.X, newStartPosition.Y, Lens.Width, Lens.Height);
        }
    }
}
