using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public static class Camera
    {
        public static Rectangle Lens { get; set; }

        public static bool IsWithinLens(int tileSize, Point location)
        {
            var spriteRectangle = new Rectangle((int)location.X*tileSize, (int)location.Y*tileSize, tileSize, tileSize);
            return Lens.Intersects(spriteRectangle);
        }

        public static void UpdateLensPosition(Point newStartPosition)
        {
            Lens = new Rectangle(newStartPosition.X, newStartPosition.Y, Lens.Width, Lens.Height);
        }

        public static Point GetGridCoordsWherePixelLocationIs(int tileSize, Point pixelLocation)
        {
            int x = (pixelLocation.X + Lens.X)/tileSize;
            int y = (pixelLocation.Y + Lens.Y)/tileSize;
            return new Point(x, y);
        }
    }
}