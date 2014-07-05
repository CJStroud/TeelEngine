using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface IEntity : ISprite
    {
        Point Location { get; set; }
        void Interact();
        void Render(SpriteBatch spriteBatch);
    }
}