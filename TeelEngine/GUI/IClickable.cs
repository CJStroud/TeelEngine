using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine.Gui
{
    public delegate void OnClickEvent(object sender, OnClickEventArgs e);

    public sealed class OnClickEventArgs : EventArgs
    {
        public MouseState MouseState { get; set; }
    }

    public delegate void OnPressEvent(object sender, OnPressEventArgs e);

    public sealed class OnPressEventArgs : EventArgs
    {
        
    }

    public delegate void OnReleaseEvent(object sender, OnReleaseEventArgs e);

    public sealed class OnReleaseEventArgs : EventArgs
    {
        
    }

    interface IClickable
    {
        event OnClickEvent OnClickHandler;

        event OnPressEvent OnPressHandler;

        event OnReleaseEvent OnReleaseHandler;

        void OnClick(OnClickEventArgs e);

        void OnPress(OnPressEventArgs e);

        void OnRelease(OnReleaseEventArgs e);

    }

}
