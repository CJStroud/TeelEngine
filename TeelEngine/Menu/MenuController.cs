using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class MenuController : IController<IMenu>, IRender
    {
        public Dictionary<string, IMenu> Items { get; set; }

        public void Add(string name, IMenu item)
        {
            if (!Items.ContainsKey(name))
            {
                Items.Add(name, item);
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            Console.Write("Rendering Menu");
        }        
    }
}