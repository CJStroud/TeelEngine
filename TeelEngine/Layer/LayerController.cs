using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class LayerController : IController<ILayer>, IRender
    {
        public Dictionary<string, ILayer> Items { get; set; }
        public LayerController()
        {
            Items = new Dictionary<string, ILayer>();
        }

        public void Add(string name, ILayer item)
        {
            if (!Items.ContainsKey(name))
            {
                Items.Add(name, item);
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var layer in Items.Values)
            {
                layer.Render(spriteBatch);
            }
        }

        public EntityLayer GetEntityLayer()
        {
            ILayer entityLayer;
            Items.TryGetValue("EntityLayer", out entityLayer);
            return entityLayer as EntityLayer;
        }

        public IEnumerable<TerrainLayer> GetTerrainLayers()
        {
            return Items.OfType<TerrainLayer>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var layer in Items.Values)
            {
                layer.Update(gameTime);
            }
        }

        public void Initialize()
        {
            foreach (var layer in Items.Values)
            {
                layer.Initialize();
            }
        }
    }
}
