using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ISprite
    {
        Vector2 Location { get; set; }
        string TextureName { get; set; }
        int Index { get; set; }
        Vector2 ScreenPosition { get; set; }
        void Render(SpriteBatch spriteBatch);
        void Render(SpriteBatch spriteBatch, Vector2 screenLocation);
        void Update(GameTime gameTime);
        void Initialize();
    }
}
