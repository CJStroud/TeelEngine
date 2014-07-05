using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class Camera
    {
        public Rectangle Lens { get; set; }


        public Camera(Point startPoint, int pixelWidth, int pixelHeight)
        {
            Lens = new Rectangle(startPoint.X, startPoint.Y, pixelWidth, pixelHeight);
        }

        public void Render(SpriteBatch spriteBatch, IEnumerable<ISprite> sprites)
        {
            //spriteBatch.Draw(texture, new Rectangle(0,0,100,100), Color.Chocolate);
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
        private Rectangle ConvertLocationFromGrid(Vector2 location)
        {
            return new Rectangle(
                (int)(location.X * Globals.TileSize), 
                (int)(location.Y * Globals.TileSize), 
                Globals.TileSize, 
                Globals.TileSize);
        }

        public void UpdatLensPosition(Point newStartPosition)
        {
            Lens = new Rectangle(newStartPosition.X, newStartPosition.Y, Lens.Width, Lens.Height);
        }
    }
}
