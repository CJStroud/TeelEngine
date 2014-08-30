using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GUI
{
    public class GuiButton : Gui
    {
        #region Constructors

        public GuiButton(Point location, int width, int height) : this(null, location, width, height)
        {
        }

        public GuiButton(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
        }

        #endregion
    }
}
