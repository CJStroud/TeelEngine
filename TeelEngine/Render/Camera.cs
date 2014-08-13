using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public static class Camera
    {
        public static Rectangle Lens { get; set; }
        /*public static void Render(SpriteBatch spriteBatch, IEnumerable<ISprite> sprites)
        {
            var lens = Lens;
            lens.Width += (Globals.TileSize * 2);
            lens.Height += (Globals.TileSize * 2);
            lens.X -= Globals.TileSize;
            lens.Y -= Globals.TileSize;

            IEnumerable<ISprite> viewableSprites = GetViewableSprites(sprites, lens);
            foreach (var sprite in viewableSprites)
            {
                var spriteScreenLocation = sprite.ScreenPosition;
                spriteScreenLocation.X -= Lens.X;
                spriteScreenLocation.Y -= Lens.Y;
                sprite.Render(spriteBatch, spriteScreenLocation);

            }
        }*/

        public static bool IsWithinLens(int tileSize, Point location)
        {
            var spriteRectangle = new Rectangle(location.X, location.Y, tileSize, tileSize);
            return Camera.Lens.Intersects(spriteRectangle);
        }

        public static void UpdateLensPosition(Point newStartPosition)
        {
            Lens = new Rectangle(newStartPosition.X, newStartPosition.Y, Lens.Width, Lens.Height);
        }
    }
}
