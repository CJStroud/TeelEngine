using System.Xml;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class CollisionTile : ITile
    {
        public Vector2 Location { get; set; }

        public XmlWriter Save(XmlWriter writer)
        {
            return writer;
        }

        public void Update(Level level, GameTime gameTime)
        {
            
        }
    }
}