using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GUI
{
    public class BaseGuiButton : BaseGui
    {
        #region Constructors

        public BaseGuiButton(Point location, int width, int height) : this(null, location, width, height)
        {
        }

        public BaseGuiButton(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
        }

        #endregion
    }
}
