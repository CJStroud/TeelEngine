using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class EntityLayer : ILayer
    {
        public List<IEntity> Entities { get; set; }

        public static List<IEntity> EntityList { get; set; } 

        public int Priority { get; private set; }
        public Texture2D SpriteSheet { get; set; }

        public EntityLayer()
        {
            Entities = new List<IEntity>();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            Camera.Render(spriteBatch, Entities);
        }

        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity);
        }

        public void Move(int entityindex, Vector2 Location)
        {
            Entities[entityindex].Location = Location;
        }

    }
}
