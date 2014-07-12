using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public static class Camera
    {
        public static Rectangle Lens { get; set; }
        public static void Render(SpriteBatch spriteBatch, IEnumerable<ISprite> sprites)
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
        }

        public static IEnumerable<ISprite> GetViewableSprites(IEnumerable<ISprite> sprites, Rectangle lens)
        {
            var viewableSprites = new List<ISprite>();
            foreach (var sprite in sprites)
            {
                var spriteRectangle = ConvertLocationFromGrid(sprite.Location);
                if (lens.Intersects(spriteRectangle))
                {
                    viewableSprites.Add(sprite);
                }
            }
            return viewableSprites;
        }

        public static IEnumerable<Vector2> GetViewableLocations(List<Vector2> locations, Rectangle lens)
        {
            var viewableLocations = new List<Vector2>();
            foreach (var location in locations)
            {
                var rectangle = ConvertLocationFromGrid(location);
                if (lens.Intersects(rectangle))
                {
                    viewableLocations.Add(location);
                }
            }
            return viewableLocations;
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
            Lens = new Rectangle(newStartPosition.X, newStartPosition.Y, Lens.Width, Lens.Height);
        }
    }
}
