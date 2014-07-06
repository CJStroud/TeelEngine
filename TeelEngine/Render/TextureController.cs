using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class TextureController : IController<ITexture>
    {
        public Dictionary<string, ITexture> Items { get; set; }

        public TextureController()
        {
            Items = new Dictionary<string, ITexture>();
        }

        public void Add(string name, ITexture item)
        {
            if (!Items.ContainsKey(name))
            {
                Items.Add(name, item);
            }
        }

        public ITexture Get(string name)
        {
            if (Items.ContainsKey(name))
            {
                return Items[name];
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            IEnumerable<AnimatedTexture> animatedTextures = Items.Values.OfType<AnimatedTexture>();

            foreach (AnimatedTexture texture in animatedTextures)
            {
                var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
                texture.NextFrame(elapsed);
            }
        }
    }
}
