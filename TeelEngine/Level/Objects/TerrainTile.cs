using System.Configuration;
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
    }
}