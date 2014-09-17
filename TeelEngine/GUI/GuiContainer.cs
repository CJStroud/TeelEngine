using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.Gui
{
    public class GuiContainer : BaseGui, IClickable
    {
        #region Properties

        private List<BaseGui> Children { get; set; }

        #endregion

        #region Constructors

        public GuiContainer(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
            Children = new List<BaseGui>();
        }

        public GuiContainer(Point location, int width, int height) : this((Texture2D)null, location, width, height)
        {
        }

        public GuiContainer(List<BaseGui> children, Point location, int width, int height) : this(location, width, height)
        {
            Children = children;
        }

        public GuiContainer(Texture2D texture, Vector2 location, float width, float height)
            : base(texture, location, width, height)
        {
            Children = new List<BaseGui>();
        }

        public GuiContainer(Vector2 location, float width, float height)
            : this(null, location, width, height){}

        public GuiContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : base(texture, location, width, height, maxWidth, maxHeight)
        {
            Children = new List<BaseGui>();
        }

        public GuiContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : this(null, location, width, height, maxWidth, maxHeight){}

        #endregion

        #region Events

        #region Event Handlers

        public event OnClickEvent OnClickHandler;
        public event OnPressEvent OnPressHandler;
        public event OnReleaseEvent OnReleaseHandler;

        #endregion

        #region Event Methods

        public void OnClick(OnClickEventArgs e)
        {
            OnClickHandler.Invoke(this, e);
        }

        public void OnPress(OnPressEventArgs e)
        {
            OnPressHandler.Invoke(this, e);
        }

        public void OnRelease(OnReleaseEventArgs e)
        {
            OnReleaseHandler.Invoke(this, e);
        }

        #endregion

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
