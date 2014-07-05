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
        public Rectangle ViewedRectangle { get; set; }


        public Camera(Point startPoint, int pixelWidth, int pixelHeight)
        {
            ViewedRectangle = new Rectangle(startPoint.X, startPoint.Y, pixelWidth, pixelHeight);
        }

        public void Render(SpriteBatch spriteBatch, List<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                var entityRectangle = ConvertLocationFromGrid(entity.Location);

            }
        }

        private int GetTileWidth()
        {
            return PixelWidth/Globals.TileSize;
        }

        private int GetTileHeight()
        {
            return PixelHeight/Globals.TileSize;
        }

        private Rectangle ConvertLocationFromGrid(Point location)
        {
            return new Rectangle(
                location.X * Globals.TileSize, 
                location.Y * Globals.TileSize, 
                Globals.TileSize, 
                Globals.TileSize);
        }
    }
}
