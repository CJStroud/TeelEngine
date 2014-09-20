using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Input;

namespace TeelEngine.Gui
{
    public class GuiScreen : GuiContainer
    {
        #region Constructors

        public GuiScreen(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
        }

        public GuiScreen(Point location, int width, int height) : base(location, width, height)
        {
        }

        public GuiScreen(List<BaseGui> children, Point location, int width, int height) : base(children, location, width, height)
        {
        }

        public GuiScreen(Texture2D texture, Vector2 location, float width, float height) : base(texture, location, width, height)
        {
        }

        public GuiScreen(Vector2 location, float width, float height) : base(location, width, height)
        {
        }

        public GuiScreen(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(texture, location, width, height, maxWidth, maxHeight)
        {
        }

        public GuiScreen(Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(location, width, height, maxWidth, maxHeight)
        {
        }

        #endregion

        public override void Update()
        {
            base.Update();

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
                        clickableGui.CurrentState = State.Enter;

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

                #region State.Enter
                case State.Enter:

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
                        clickableGui.CurrentState = State.Enter;

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

    }
}
