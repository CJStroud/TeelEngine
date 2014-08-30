using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GUI
{
    public class GuiContainer : Gui
    {
        #region Properties

        private List<Gui> Children { get; set; }

        #endregion

        #region Constructors

        public GuiContainer(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
            Children = new List<Gui>();
        }

        public GuiContainer(Point location, int width, int height) : this((Texture2D)null, location, width, height)
        {
        }

        public GuiContainer(List<Gui> children, Point location, int width, int height) : this(location, width, height)
        {
            Children = children;
        }

        public GuiContainer(Texture2D texture, Vector2 location, float width, float height)
            : base(texture, location, width, height)
        {
            Children = new List<Gui>();
        }

        public GuiContainer(Vector2 location, float width, float height)
            : this(null, location, width, height){}

        public GuiContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : base(texture, location, width, height, maxWidth, maxHeight)
        {
            Children = new List<Gui>();
        }

        public GuiContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : this(null, location, width, height, maxWidth, maxHeight){}

        #endregion

        public void AddGui(Gui childGui)
        {
            childGui.ParentContainer = this;
            Children.Add(childGui);
        }

        public void RemoveGui(Gui childGui)
        {
            Children.Remove(childGui);
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
