using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TeelEngine.Render;

namespace TeelEngine.Level
{
    public class EntityTile : ITile, IAnimatable
    {
        public int TextureId { get; set; }
        public string AssetName { get; set; }
        public Dictionary<string, int> Animations { get; set; }
        public string CurrentAnimation { get; set; }
    }
}