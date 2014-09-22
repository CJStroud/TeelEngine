using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeelEngine
{
    public enum Direction : uint
    {
        North = 0,
        East = 1,
        West =  2,
        South = 3,
        None = 4
    }

    public enum MouseButtons
    {
        LeftButton = 0,
        RightButton,
        MiddleButton,
    }
}
