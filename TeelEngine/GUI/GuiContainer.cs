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
        public event OnEnterEvent OnEnterHandler;
        public event OnLeaveEvent OnLeaveHandler;
        public event WhileDownEvent WhileDownHandler;
        public event WhileHoverEvent WhileHoverHandler;

        #endregion

        #region Event Methods

        public void OnClick(OnClickEventArgs e)
        {
            Console.WriteLine("Clicked");

            if (OnClickHandler != null)
            {
                OnClickHandler.Invoke(this, e);
            }
        }

        public void OnPress(OnPressEventArgs e)
        {
            Console.WriteLine("Pressed");

            if (OnPressHandler != null)
            {
                OnPressHandler.Invoke(this, e);
            }
        }

        public void OnRelease(OnReleaseEventArgs e)
        {
            Console.WriteLine("Released");

            if (OnReleaseHandler != null)
            {
                OnReleaseHandler.Invoke(this, e);
            }
        }

        public void OnEnter(OnEnterEventArgs e)
        {
            Console.WriteLine("Entered");

            if (OnEnterHandler != null)
            {
                OnEnterHandler.Invoke(this, e);
            }
        }

        public void OnLeave(OnLeaveEventArgs e)
        {
            Console.WriteLine("Left");

            if (OnLeaveHandler != null)
            {
                OnLeaveHandler.Invoke(this, e);
            }
        }

        public void WhileHeld(WhileDownEventArgs e)
        {
            //Console.WriteLine("Holding");

            if (WhileDownHandler != null)
            {
                WhileDownHandler.Invoke(this, e);
            }
        }

        public void WhileHovering(WhileHoverEventArgs e)
        {
            //Console.WriteLine("Hovering");

            if (WhileHoverHandler != null)
            {
                WhileHoverHandler.Invoke(this, e);
            }
        }

        public State CurrentState { get; set; }

        #endregion

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

            if (DepthLevel != 0) return;

            Rectangle mouseRectangle = MouseHandler.GetMouseRectangle();

            var intersectingChildren = new List<BaseGui>();

            GetMouseIntersectChildren(mouseRectangle, intersectingChildren);

            if (intersectingChildren.Count <= 0) return;

            intersectingChildren = intersectingChildren.OrderByDescending(x => x.DepthLevel).ThenByDescending(x => x.Priority).ToList();

            var clickableGui = (IClickable)intersectingChildren.First();

            State currentState = clickableGui.CurrentState;

            switch (currentState)
            {
                #region State.None
                case State.None:

                    if (MouseHandler.IsMouseButtonPressed())
                    {
                        clickableGui.CurrentState = State.Pressed;

                        clickableGui.OnPress(new OnPressEventArgs());
                    }
                    else
                    {
                        clickableGui.CurrentState = State.Hover;

                        clickableGui.OnEnter(new OnEnterEventArgs());
                    }
                    break;
                #endregion

                #region State.Pressed
                case State.Pressed:

                    if (MouseHandler.IsMouseButtonHeld())
                    {
                        clickableGui.CurrentState = State.Held;

                        clickableGui.WhileHeld(new WhileDownEventArgs());
                    }
                    else
                    {
                        clickableGui.CurrentState = State.Released;

                        clickableGui.OnRelease(new OnReleaseEventArgs());

                        clickableGui.OnClick(new OnClickEventArgs());
                    }

                    break;
                #endregion

                #region State.Released
                case State.Released:

                    if (MouseHandler.IsMouseButtonPressed())
                    {
                        clickableGui.CurrentState = State.Pressed;

                        clickableGui.OnPress(new OnPressEventArgs());
                    }
                    else
                    {
                        clickableGui.CurrentState = State.Hovering;

                        clickableGui.WhileHovering(new WhileHoverEventArgs());
                    }

                    break;
                #endregion

                #region State.Hover
                case State.Hover:

                    if (MouseHandler.IsMouseButtonPressed())
                    {
                        clickableGui.CurrentState = State.Pressed;

                        clickableGui.OnPress(new OnPressEventArgs());
                    }
                    else
                    {
                        clickableGui.CurrentState = State.Hovering;

                        clickableGui.WhileHovering(new WhileHoverEventArgs());
                    }

                    break;
                #endregion

                #region State.Leave

                case State.Leave:

                    if (MouseHandler.IsMouseButtonPressed())
                    {
                        clickableGui.CurrentState = State.Pressed;

                        clickableGui.OnPress(new OnPressEventArgs());
                    }
                    else
                    {
                        clickableGui.CurrentState = State.Hover;

                        clickableGui.OnEnter(new OnEnterEventArgs());
                    }

                    break;

                #endregion

                #region State.Held

                case State.Held:

                    if (MouseHandler.IsMouseButtonReleased())
                    {
                        clickableGui.CurrentState = State.Released;

                        clickableGui.OnClick(new OnClickEventArgs());

                        clickableGui.OnRelease(new OnReleaseEventArgs());
                    }
                    else
                    {
                        clickableGui.WhileHeld(new WhileDownEventArgs());
                    }

                    break;

                #endregion

                #region State.Hovering

                case State.Hovering:

                    if (MouseHandler.IsMouseButtonPressed())
                    {
                        clickableGui.CurrentState = State.Pressed;

                        clickableGui.OnPress(new OnPressEventArgs());
                    }
                    else
                    {
                        clickableGui.WhileHovering(new WhileHoverEventArgs());
                    }

                    break;

                #endregion

                default:
                    throw new ArgumentOutOfRangeException();
            }

            MouseHandler.PreviousMouseState = MouseHandler.CurrentMouseState;
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
                    var clickable = (IClickable) child;

                    if (clickable.CurrentState == State.Hovering || clickable.CurrentState == State.Hover)
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
