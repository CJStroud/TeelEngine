using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeelEngine.Input;

namespace TeelEngine.Gui
{
    public class GuiContainer : BaseGui
    {
        #region Properties

        protected internal List<BaseGui> Children { get; set; }

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

        #region Private Globals

        private int _currentPriority = 0;

        #endregion

        public void AddGui(BaseGui childBaseGui)
        {
            childBaseGui.ParentContainer = this;
            Console.WriteLine("Set child depth level to: " + (DepthLevel + 1));
            childBaseGui.DepthLevel = DepthLevel + 1;
            childBaseGui.Priority = _currentPriority;
            _currentPriority += 1;
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

        public void GetMouseIntersectChildren(Rectangle mouseRectangle, List<BaseGui> intersectingGuis)
        {
            foreach (var child in Children)
            {
                var container = child as GuiContainer;

                if (container != null)
                {
                    container.GetMouseIntersectChildren(mouseRectangle, intersectingGuis);
                }

                if (!(child is IClickable)) continue;
                if (!child.BoundingRectangle.Intersects(mouseRectangle))
                {
                    var clickable = (IClickable)child;

                    if (clickable.CurrentState == State.Hovering || clickable.CurrentState == State.Enter)
                    {
                        clickable.CurrentState = State.Leave;

                        clickable.OnLeave(new OnLeaveEventArgs());
                    }
                    else if (clickable.CurrentState == State.Leave)
                    {
                        clickable.CurrentState = State.None;
                    }
                    continue;
                }

                intersectingGuis.Add(child);

            }
        }
    }
}
