using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeelEngine.Render;

namespace TeelEngine.Gui
{
    public class GuiGameContainer : GuiContainer, IClickable
    {
        #region Constructor

        public GuiGameContainer(Texture2D texture, Point location, int width, int height)
            : base(texture, location, width, height)
        {
        }

        public GuiGameContainer(Point location, int width, int height)
            : base(location, width, height)
        {
        }

        public GuiGameContainer(List<BaseGui> children, Point location, int width, int height)
            : base(children, location, width, height)
        {
        }

        public GuiGameContainer(Texture2D texture, Vector2 location, float width, float height)
            : base(texture, location, width, height)
        {
        }

        public GuiGameContainer(Vector2 location, float width, float height)
            : base(location, width, height)
        {
        }

        public GuiGameContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : base(texture, location, width, height, maxWidth, maxHeight)
        {
        }

        public GuiGameContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : base(location, width, height, maxWidth, maxHeight)
        {
        }

        #endregion 

        #region Overriden Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            Camera.Lens = new Rectangle(Camera.Lens.X, Camera.Lens.Y, BoundingRectangle.Width, BoundingRectangle.Height);

            if (Visible){
                if (OnDrawHandler != null)
                {
                    OnDrawHandler.Invoke(this, new DrawEventArgs(spriteBatch));
                }
            }
            base.Draw(spriteBatch);
        }

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
        public event OnDrawEvent OnDrawHandler;

        #endregion

        #region Event Methods

        public void OnClick(OnClickEventArgs e)
        {
            if (OnClickHandler != null)
            {
                OnClickHandler.Invoke(this, new OnClickEventArgs());
            }
        }

        public void OnPress(OnPressEventArgs e)
        {
            if (OnPressHandler != null)
            {
                OnPressHandler.Invoke(this, new OnPressEventArgs());
            }
        }

        public void OnRelease(OnReleaseEventArgs e)
        {
            if (OnReleaseHandler != null)
            {
                OnReleaseHandler.Invoke(this, new OnReleaseEventArgs());
            }
        }

        public void OnEnter(OnEnterEventArgs e)
        {
            if (OnEnterHandler != null)
            {
                OnEnterHandler.Invoke(this, new OnEnterEventArgs());
            }
        }

        public void OnLeave(OnLeaveEventArgs e)
        {
            if (OnLeaveHandler != null)
            {
                OnLeaveHandler.Invoke(this, new OnLeaveEventArgs());
            }
        }

        public void WhileHeld(WhileDownEventArgs e)
        {
            if (WhileDownHandler != null)
            {
                WhileDownHandler.Invoke(this, new WhileDownEventArgs());
            }
        }

        public void WhileHovering(WhileHoverEventArgs e)
        {
            if (WhileHoverHandler != null)
            {
                WhileHoverHandler.Invoke(this, new WhileHoverEventArgs());
            }
        }

        #endregion

        public State CurrentState { get; set; }

        #endregion
    }
}
