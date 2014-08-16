using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class SpriteSheet
    {
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public int TileSize { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public SpriteSheet(string assetName, int tileSize, Texture2D texture)
        {
            TileSize = tileSize;
            Texture = texture;
            Name = assetName;

            RowCount = Texture.Height/TileSize;
            ColumnCount = Texture.Width/TileSize;

        }


        public Point GetTileLocation(int index)
        {
            int x = index % ColumnCount;
            int y = index / ColumnCount;

            return new Point(x, y);
        }

        public Rectangle GetTileRectangle(int index)
        {
            Point location = GetTileLocation(index);

            return GetTileRectangle(location);
        }

        public Rectangle GetTileRectangle(Point location)
        {
            return new Rectangle(location.X * TileSize, location.Y * TileSize, TileSize, TileSize);
        }

    }
}
