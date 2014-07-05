using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class Path
    {
        private List<Direction> _pathList;
        public Point StartPoint;

        public Path(Point start)
        {
            StartPoint = start;
            _pathList = new List<Direction>();
        }

        public void Add(Direction direction)
        {
            _pathList.Add(direction);
        }

        private void Remove(int index)
        {
            _pathList.RemoveAt(index);
        }

        public Direction GetNextDirection()
        {
            Direction direction = _pathList.First();
            this.Remove(0);
            return direction;
        }
    }
}
