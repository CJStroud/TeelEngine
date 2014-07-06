using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface IEntity : ISprite
    {
        void Interact();
    }
}