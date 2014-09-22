using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeelEngine.Path
{
    public class Path
    { 
        public List<PathNode> Nodes { get; set; }

        public Direction GetNextDirection()
        {
            Direction direction = Nodes[0].Direction;
            Nodes.RemoveAt(0);
            return direction;
        }
    }
}
