using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class LayerController : IController<ILayer>, IRender
    {
        public List<ILayer> Items { get; private set; }

        public LayerController()
        {
            Items = new List<ILayer>();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var layer in Items)
            {
                layer.Render(spriteBatch);
            }
        }

        public void Add(ILayer item)
        {
            Items.Add(item);
            Items = Items.OrderBy(itm => itm.Priority).ToList();
        }

        public EntityLayer GetEntityLayer()
        {
            return Items.OfType<EntityLayer>().FirstOrDefault();
        }

        public IEnumerable<TerrainLayer> GetTerrainLayers()
        {
            return Items.OfType<TerrainLayer>();
        }
    }
}
