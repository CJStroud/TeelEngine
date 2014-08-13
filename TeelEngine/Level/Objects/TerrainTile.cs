using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class TerrainTile : ITile, IRenderable
    {
        public Point Location { get; private set; }
        public int TextureId { get; set; }
    }
}