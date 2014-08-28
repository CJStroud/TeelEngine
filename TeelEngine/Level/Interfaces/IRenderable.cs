using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public interface IRenderable
    {
        ITexture Texture { get; set; }

        float Rotation { get; set; }

        int Layer { get; set; }

        Vector2 Location { get; set; }

        Vector2 Offset { get; set; }
    }
}