using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class TerrainTile : ITile, IRenderable
    {
        public int TextureId { get; set; }
        public string AssetName { get; set; }
    }
}