using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine.Input
{
    public static class MouseHandler
    {
        public static MouseState PreviousMouseState;

        public static MouseState CurrentMouseState
        {
            get { return Mouse.GetState(); }
        }

        public static bool IsMouseButtonClicked()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed ||
                   CurrentMouseState.MiddleButton == ButtonState.Pressed ||
                   CurrentMouseState.RightButton == ButtonState.Pressed;
        }

        public static Rectangle GetMouseRectangle()
        {
            return new Rectangle(CurrentMouseState.X, CurrentMouseState.Y, 1, 1);
        }
    }
}
