using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class EntityLayer : ILayer
    {
        public List<IEntity> Entities { get; set; }

        public static List<IEntity> EntityList { get; set; } 

        public EntityLayer()
        {
            Entities = new List<IEntity>();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            Camera.Render(spriteBatch, Entities);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }
        }

        public void Initialize()
        {
            foreach (var entity in Entities)
            {
                entity.Initialize();
            }
        }

        public void Add(IEntity entity)
        {
            Entities.Add(entity);
        }

        public IEnumerable<T> GetOfType<T>()
        {
            return Entities.OfType<T>();
        }

        public IEnumerable<IEntity> GetEntitiesExcluding(IEntity entity)
        {
            return Entities.FindAll(delegate(IEntity entity1)
            {
                bool b = entity1.Location != entity.Location;
                return b;
            });
        } 
    }
}
