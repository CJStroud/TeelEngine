using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ILayer : IRender
    {
        int Priority { get; }
        Texture2D SpriteSheet { get; set; }
    }
}