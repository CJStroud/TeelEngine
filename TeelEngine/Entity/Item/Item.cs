using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class Item : IEntity
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Location { get; set; }
        public void Interact()
        {
            
        }

        public void Render(SpriteBatch spriteBatch)
        {
            
        }

        public void Render(SpriteBatch spriteBatch, Rectangle drawRectangle)
        {
            
        }
    }
}
