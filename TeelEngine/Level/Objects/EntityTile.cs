using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class EntityTile : ITile, IRenderable
    {
        public int TextureId { get; set; }
        public Point Location { get; private set; }
    }
}