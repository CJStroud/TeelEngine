using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GUI
{
    public class BaseGuiContainer : BaseGui
    {
        #region Properties

        private List<BaseGui> Children { get; set; }

        #endregion

        #region Constructors

        public BaseGuiContainer(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
            Children = new List<BaseGui>();
        }

        public BaseGuiContainer(Point location, int width, int height) : this((Texture2D)null, location, width, height)
        {
        }

        public BaseGuiContainer(List<BaseGui> children, Point location, int width, int height) : this(location, width, height)
        {
            Children = children;
        }

        public BaseGuiContainer(Texture2D texture, Vector2 location, float width, float height)
            : base(texture, location, width, height)
        {
            Children = new List<BaseGui>();
        }

        public BaseGuiContainer(Vector2 location, float width, float height)
            : this(null, location, width, height){}

        public BaseGuiContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : base(texture, location, width, height, maxWidth, maxHeight)
        {
            Children = new List<BaseGui>();
        }

        public BaseGuiContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : this(null, location, width, height, maxWidth, maxHeight){}

        #endregion

        public void AddGui(BaseGui childBaseGui)
        {
            childBaseGui.ParentContainer = this;
            Children.Add(childBaseGui);
        }

        public void RemoveGui(BaseGui childBaseGui)
        {
            Children.Remove(childBaseGui);
        }

        public void RemoveGui(int index)
        {
            if (index < 0 || index >= Children.Count) return;

            Children.RemoveAt(index);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var child in Children)
            {
                child.Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            base.Update();

            foreach (var child in Children)
            {
                child.Update();
            }
        }
    }
}
