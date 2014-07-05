using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TeelEngine
{
    public class Unit : IEntity
    {
        public Texture2D Texture { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Vector2 Location { get; set; }

        public Unit(Texture2D texture)
        {
            Texture = texture;
        }

        public void Interact()
        {
            
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(100, 100, Globals.TileSize, Globals.TileSize), Color.White);
        }

        public void Render(SpriteBatch spriteBatch, Rectangle drawRectangle)
        {
            spriteBatch.Draw(Texture, drawRectangle, Color.White);
        }
    }
}
