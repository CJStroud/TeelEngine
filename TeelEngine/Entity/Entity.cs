using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TeelEngine.Level;
using TeelEngine.Render;

namespace TeelEngine
{
    public class Entity : IAnimatable
    {
        public Point Location { get; set; }

        public Dictionary<string, int> Animations { get; set; }

        public string CurrentAnimation { get; set; }

        public ITexture Texture { get; set; }

        public int EntityId { get; set; }


        public override void Update(Level.Level level, GameTime gameTime)
        {
            
        }

    }
}
