using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeelEngine
{
    public enum Direction
    {
        North = 0,
        East = 1,
        South =  ~North,
        West = ~East,
        None = 4
    }
}
