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
            spriteBatch.Draw(Globals.texture, new Rectangle(0,0,Lens.Width,Lens.Height), Color.Chocolate);
            foreach (var entity in sprites)
            {
                var entityRectangle = ConvertLocationFromGrid(entity.Location);
                if (Lens.Intersects(entityRectangle))
                {
                    entityRectangle.X -= Lens.X;
                    entityRectangle.Y -= Lens.Y;
                    entity.Render(spriteBatch, entityRectangle);
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
