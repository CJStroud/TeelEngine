using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GUI
{
    public class BaseGuiButtonContainer : BaseGuiContainer
    {
        #region Constructors

        public BaseGuiButtonContainer(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
        }

        public BaseGuiButtonContainer(Point location, int width, int height) : base(location, width, height)
        {
        }

        public BaseGuiButtonContainer(List<BaseGui> children, Point location, int width, int height) : base(children, location, width, height)
        {
        }

        public BaseGuiButtonContainer(Texture2D texture, Vector2 location, float width, float height) : base(texture, location, width, height)
        {
        }

        public BaseGuiButtonContainer(Vector2 location, float width, float height) : base(location, width, height)
        {
        }

        public BaseGuiButtonContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(texture, location, width, height, maxWidth, maxHeight)
        {
        }

        public BaseGuiButtonContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(location, width, height, maxWidth, maxHeight)
        {
        }

        #endregion
    }
}
