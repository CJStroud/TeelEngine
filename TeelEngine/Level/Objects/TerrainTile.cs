using System.Configuration;
using System.Net.Mime;
using System.Xml;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class TerrainTile : ITile, IRenderable
    {
        public ITexture Texture { get; set; }
        public int Layer { get; set; }
        public Vector2 Location { get; set; }

        public Vector2 Offset { get; set; }
        public float Rotation { get; set; }

        public void Update(Level level, GameTime gameTime)
        {
        }

        public XmlWriter Save(XmlWriter writer)
        {
            writer.WriteStartElement("TeelEngine.Level.TerrainTile");

            writer.WriteElementString("Location", Location.X + ", " + Location.Y);
            writer.WriteElementString("Layer", Layer.ToString());

            writer.WriteStartElement("Texture");
            writer.WriteElementString("Asset", Texture.AssetName);
            writer.WriteElementString("Id", Texture.TextureId.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();

            return writer;
        }


    }
}