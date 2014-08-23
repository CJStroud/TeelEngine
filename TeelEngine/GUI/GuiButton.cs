using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine
{
    public delegate void OnMouseDelegate(MouseButton button);

    public enum MouseButton
    {
        Left = 0,
        Right,
        Middle,
        None
    }

    public class GuiButton : Gui
    {
        private OnMouseDelegate _onMouseDown;

        private OnMouseDelegate _onMouseUp;

        private MouseState _mouseState;

        private MouseButton _previousPressedButton;

        public GuiButton(Texture2D texture, Point location, OnMouseDelegate onMouseDown, OnMouseDelegate onMouseUp)
            : this(texture, location, texture.Width, texture.Height, onMouseDown, onMouseUp)
        {
        }

        public GuiButton(Texture2D texture, Point location, int width, int height, OnMouseDelegate onMouseDown, OnMouseDelegate onMouseUp): base(texture, location, width, height)
        {
            _onMouseDown = onMouseDown;
            _onMouseUp = onMouseUp;
            _mouseState = Mouse.GetState();
        }

        public override void Update()
        {
            var mouseRectangle = new Rectangle(_mouseState.X, _mouseState.Y, 1, 1);

            if (mouseRectangle.Intersects(this.BoundingRectangle))
            {
                MouseButton button = IsPressed();

                if(button != MouseButton.None)
                {
                    OnMouseDown(button);
                }

                if (button != _previousPressedButton)
                {
                    OnMouseUp(_previousPressedButton);
                }

                _previousPressedButton = button;

            }
            base.Update();
        }

        public void OnMouseDown(MouseButton button)
        {
            _onMouseDown(button);
        }

        public void OnMouseUp(MouseButton button)
        {
            _onMouseUp(button);
        }

        private MouseButton IsPressed()
        {
            if (_mouseState.LeftButton == ButtonState.Pressed) return MouseButton.Left;
            if (_mouseState.RightButton == ButtonState.Pressed) return MouseButton.Right;
            if (_mouseState.MiddleButton == ButtonState.Pressed) return MouseButton.Middle;

            return MouseButton.None;
        }
    }
}
