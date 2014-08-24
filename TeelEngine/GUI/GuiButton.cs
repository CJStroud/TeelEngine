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
    public class OnPressEventArgs : EventArgs
    {
        public InputType Type { get; set; }
        public int Keypressed { get; set; }
    }

    public enum InputType
    {
        Gamepad = 0,
        Mouse,
        Keyboard
    }

    public enum MouseButton
    {
        Left = 0,
        Right,
        Middle,
        None
    }

    public class GuiButton : Gui
    {
        private EventHandler<OnPressEventArgs> _onButtonDown;

        private EventHandler<OnPressEventArgs> _onButtonUp;

        private EventHandler _onHover;

        private int _previousPressedButton;

        public GuiButton(Texture2D texture, Point location, OnPressDelegate onButtonDown, OnPressDelegate onButtonUp, OnHoverDelegate onHover)
            : this(texture, location, texture.Width, texture.Height, onButtonDown, onButtonUp, onHover)
        {
        }

        public GuiButton(Texture2D texture, Point location, int width, int height, EventHandler<OnPressEventArgs> onButtonDown, EventHandler<OnPressEventArgs> onButtonUp, EventHandler onHover)
            : base(texture, location, width, height)
        {
            _onButtonDown += onButtonDown;
            _onButtonUp += onButtonUp;
            _onHover += onHover;
        }

        public override void Update()
        {


            base.Update();
        }

        public virtual void OnButtonDown(object sender, OnPressEventArgs eventArgs)
        {
            _onButtonDown(sender, eventArgs);
        }

        public virtual void OnButtonUp(object sender, OnPressEventArgs eventArgs)
        {
            _onButtonUp(sender, eventArgs);
        }

    }
}
