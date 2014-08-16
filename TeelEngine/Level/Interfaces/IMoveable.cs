using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level.Interfaces
{
    interface IMoveable
    {
        Vector2 Offset { get; set; }
        Point NewLocation { get; set; }
        Direction MoveDirection { get; set; }
        float Speed { get; set; }

        void Move(Direction direction);
    }
}
