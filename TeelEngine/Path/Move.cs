using System;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class Move
    {
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public int StepsFromGoal { get; private set; }

        public Move(Point nextLocation, Point goalPoint, Direction direction)
        {
            int xDirec = Math.Abs(nextLocation.X - goalPoint.X);
            int yDirec = Math.Abs(nextLocation.Y - goalPoint.Y);
            StepsFromGoal = xDirec + yDirec;
            Location = nextLocation;
            Direction = direction;
        }
    }
}