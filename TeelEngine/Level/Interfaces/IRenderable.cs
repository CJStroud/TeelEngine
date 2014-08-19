namespace TeelEngine.Level
{
    public interface IRenderable
    {
        ITexture Texture { get; set; }

        float Rotation { get; set; }
    }
}