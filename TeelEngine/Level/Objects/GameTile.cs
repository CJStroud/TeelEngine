using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TeelEngine.Level
{
    public class GameTile
    {
        public GameTile()
        {
            SubTiles = new List<ITile>();
        }
        [ContentSerializer]
        public List<ITile> SubTiles { get; private set; }
        public Point Location { get; set; }

        public void AddTile(ITile tile)
        {
            SubTiles.Add(tile);
        }

        public void Update(Level level, GameTime gameTime)
        {
            foreach (var subTile in SubTiles)
            {
                subTile.Update(level, gameTime);
            }
        }

        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement("TeelEngine.Level.GameTile");
            foreach (var subTile in SubTiles)
            {
                writer = subTile.Save(writer);
            }
            writer.WriteEndElement();
        }

        public void Load(XmlReader reader)
        {
            
        }
    }
}