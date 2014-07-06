using System.Collections.Generic;
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
    }
}
