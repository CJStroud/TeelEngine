using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class MenuController : IController<IMenu>, IRender
    {
        public int Priority { get; private set; }

        public void Render(SpriteBatch spriteBatch)
        {
            Console.Write("Rendering Menu");
        }

        public void Render(SpriteBatch spriteBatch, Camera camera)
        {
        }

        public List<IMenu> Items { get; private set; }

        public void Add(IMenu item)
        {
            Items.Add(item);
        }
    }
}