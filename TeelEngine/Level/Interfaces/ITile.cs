using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public interface ITile
    {
        void Update(Level level, GameTime gameTime);
        Vector2 Location { get; set; }
    }
}