using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ILayer : IRender
    {
        void Update(GameTime gameTime);
        void Initialize();
    }
}