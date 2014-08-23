using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TeelEngine
{
    public class Gui
    {
        public Point Location { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, Width, Height);
            }
        }

        private Texture2D _texture;

        public Gui(Texture2D texture, Point location, int width, int height)
        {
            _texture = texture;
            Location = location;
            Width = width;
            Height = height;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(Location.X, Location.Y, Width, Height), Color.White);
        }

        public virtual void Update()
        {
            
        }
    }

    
}
