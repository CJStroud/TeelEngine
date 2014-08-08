using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace UnitTests
{
    public class Move
    {
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public List<Direction> UnusableDirections { get; private set; }
        public int TotalSteps { get; private set; }
        public int XSteps { get; private set; }
        public int YSteps { get; private set; }

        public Move(Point nextLocation, Point goalPoint, Direction direction)
        {
            int xDirec = Math.Abs(nextLocation.X - goalPoint.X);
            int yDirec = Math.Abs(nextLocation.Y - goalPoint.Y);
            XSteps = xDirec;
            YSteps = yDirec;
            TotalSteps = xDirec + yDirec;
            Location = nextLocation;
            Direction = direction;
        }

        public void AddUnusableDirection(Direction direction)
        {
            if (!UnusableDirections.Contains(direction))
            {
                UnusableDirections.Add(direction);
            }
        }

    }
}