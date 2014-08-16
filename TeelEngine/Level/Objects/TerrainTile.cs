using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public class TerrainTile : ITile, IRenderable
    {
        public ITexture Texture { get; set; }

        public void Update(Level level, GameTime gameTime)
        {
            
        }
    }
}