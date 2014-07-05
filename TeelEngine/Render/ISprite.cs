using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ISprite
    {
        Vector2 Location { get; set; }
        Texture2D Texture { get; }

        void Render(SpriteBatch spriteBatch);
        void Render(SpriteBatch spriteBatch, Rectangle drawRectangle);
    }
}
