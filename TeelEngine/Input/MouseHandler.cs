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

        public static bool IsMouseButtonPressed()
        {
            return
                PreviousMouseState.LeftButton == ButtonState.Released &&
                CurrentMouseState.LeftButton == ButtonState.Pressed ||

                PreviousMouseState.RightButton == ButtonState.Released &&
                CurrentMouseState.RightButton == ButtonState.Pressed ||

                PreviousMouseState.MiddleButton == ButtonState.Released &&
                CurrentMouseState.MiddleButton == ButtonState.Pressed ||

                PreviousMouseState.XButton1 == ButtonState.Released && 
                CurrentMouseState.XButton1 == ButtonState.Pressed ||

                PreviousMouseState.XButton2 == ButtonState.Released && 
                CurrentMouseState.XButton2 == ButtonState.Pressed;
        }

        public static bool IsMouseButtonReleased()
        {
            return
                PreviousMouseState.LeftButton == ButtonState.Pressed &&
                CurrentMouseState.LeftButton == ButtonState.Released ||

                PreviousMouseState.RightButton == ButtonState.Pressed &&
                CurrentMouseState.RightButton == ButtonState.Released ||

                PreviousMouseState.MiddleButton == ButtonState.Pressed &&
                CurrentMouseState.MiddleButton == ButtonState.Released ||

                PreviousMouseState.XButton1 == ButtonState.Pressed &&
                CurrentMouseState.XButton1 == ButtonState.Released ||

                PreviousMouseState.XButton2 == ButtonState.Pressed &&
                CurrentMouseState.XButton2 == ButtonState.Released;
        }

        public static bool IsMouseButtonHeld()
        {
            return
                PreviousMouseState.LeftButton == ButtonState.Pressed &&
                CurrentMouseState.LeftButton == ButtonState.Pressed ||

                PreviousMouseState.RightButton == ButtonState.Pressed &&
                CurrentMouseState.RightButton == ButtonState.Pressed ||

                PreviousMouseState.MiddleButton == ButtonState.Pressed &&
                CurrentMouseState.MiddleButton == ButtonState.Pressed ||

                PreviousMouseState.XButton1 == ButtonState.Pressed &&
                CurrentMouseState.XButton1 == ButtonState.Pressed ||

                PreviousMouseState.XButton2 == ButtonState.Pressed &&
                CurrentMouseState.XButton2 == ButtonState.Pressed;
        }

        public static Rectangle GetMouseRectangle()
        {
            return new Rectangle(CurrentMouseState.X, CurrentMouseState.Y, 1, 1);
        }
    }
}
