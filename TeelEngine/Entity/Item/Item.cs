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
        public string TextureName { get;  set; }
        public int Index { get;  set; }
        public Vector2 ScreenPosition { get; set; }
        public Vector2 Location { get; set; }

        public void Render(SpriteBatch spriteBatch)
        {
            
        }

        public void Render(SpriteBatch spriteBatch, Vector2 screenLocation)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Initialize()
        {
            
        }


        public void Interact()
        {
            
        }
    }
}
