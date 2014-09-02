using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TeelEngine.Level;
using TeelEngine.Render;

namespace TeelEngine
{
    public class Entity : IRenderable
    {
        public Vector2 Location { get; set; }

        public SpriteTexture Texture { get; set; }

        public float Rotation { get; set; }

        public int EntityId { get; set; }

        public Vector2 Offset { get; set; }

        public string Group { get; set; }

        public int Layer { get; set; }

        public virtual void Update(Level.Level level, GameTime gameTime)
        {

        }

    }
}
