using System;
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

    public delegate void OnEnterEvent(object sender, OnEnterEventArgs e);

    public sealed class OnEnterEventArgs : EventArgs
    {
        
    }

    public delegate void OnLeaveEvent(object sender, OnLeaveEventArgs e);

    public sealed class OnLeaveEventArgs : EventArgs
    {
        
    }

    public delegate void WhileDownEvent(object sender, WhileDownEventArgs e);

    public sealed class WhileDownEventArgs : EventArgs
    {
        
    }

    public delegate void WhileHoverEvent(object sender, WhileHoverEventArgs e);

    public sealed class WhileHoverEventArgs : EventArgs
    {
        
    }

    interface IClickable
    {
        event OnClickEvent OnClickHandler;

        event OnPressEvent OnPressHandler;

        event OnReleaseEvent OnReleaseHandler;

        event OnEnterEvent OnEnterHandler;

        event OnLeaveEvent OnLeaveHandler;

        event WhileDownEvent WhileDownHandler;

        event WhileHoverEvent WhileHoverHandler;

        void OnClick(OnClickEventArgs e);

        void OnPress(OnPressEventArgs e);

        void OnRelease(OnReleaseEventArgs e);

        void OnEnter(OnEnterEventArgs e);

        void OnLeave(OnLeaveEventArgs e);

        void WhileHeld(WhileDownEventArgs e);

        void WhileHovering(WhileHoverEventArgs e);

        State CurrentState { get; set; }

    }

}
