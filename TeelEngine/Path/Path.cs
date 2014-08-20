using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class Path
    { 
        public List<PathNode> Nodes { get; set; }
        public bool ReachedEnd { get; private set; }
        private int _index;

/*
        public Direction GetNextDirection()
        {
            Direction direction = Nodes[_index].Direction;
            _index++;
            return direction;
        }
*/

        public Vector2 GetNextLocation()
        {
            var location = new Vector2(Nodes[_index].Location.X, Nodes[_index].Location.Y);
            if (Nodes[_index].Location == Nodes.Last().Location) ReachedEnd = true;
            _index++;
            return location;
        }
    }
}
