using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class PathNode
    {
        public List<Move> AvailableMoves { get; set; }
        public Direction ChosenDirection { get; set; }
        public Direction PreviousNodeDirection { get; set; }

        public Point Location { get; private set; }

        public PathNode(Direction previousDirection, Point location, Point endPoint)
        {
            PreviousNodeDirection = (Direction)((uint)previousDirection ^ 3);
            Console.WriteLine("Prev Direction: " + PreviousNodeDirection);
            Location = location;
            AvailableMoves = new List<Move>
                {
                    new Move(new Point(Location.X, Location.Y - 1), endPoint, Direction.North),
                    new Move(new Point(Location.X + 1, Location.Y), endPoint, Direction.East),
                    new Move(new Point(Location.X, Location.Y + 1), endPoint, Direction.South),
                    new Move(new Point(Location.X - 1, Location.Y), endPoint, Direction.West),
                };

            if ((uint)previousDirection != (uint)Direction.None)
            {
                int index = -1;
                for (int i = 0; i < AvailableMoves.Count; i++)
                {
                    if (AvailableMoves[i].Direction == previousDirection)
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    Console.WriteLine("Removed: " + AvailableMoves[index].Direction);
                    AvailableMoves.RemoveAt(index);
                    
                }
                
                //AvailableMoves.RemoveAll(m => m.Direction == previousDirection);
            }
            AvailableMoves = new List<Move>(AvailableMoves.OrderBy(m => m.StepsFromGoal));
        }

        public List<PathNode> GetNextNode(Point endPoint, int previousStepCount)
        {
            if (Location == endPoint)
            {
                PathNode pathNode = new PathNode((Direction)((uint)ChosenDirection ^ 3), Location, endPoint);
                List<PathNode> thePath = new List<PathNode> { pathNode };
                Console.WriteLine("This is where it should stop...");
                return thePath;
            }

            int loops = AvailableMoves.Count;
            for (int i = 0; i < loops; i++)
            {
                if (CollisionDetection.EntityCollididesWithTerrain(new Vector2(AvailableMoves[i].Location.X, AvailableMoves[i].Location.Y)))
                {
                    Console.WriteLine("Collided at Direction: " + AvailableMoves[i].Direction);
                    AvailableMoves.RemoveAt(i);
                    loops--;
                    i--;
                }
            }


            if (AvailableMoves.Count < 1)
            {
                Console.WriteLine("Going up a level : Step count / no moves");
                return null;
            }


            Console.WriteLine("D: " + AvailableMoves[0].Direction + ", C: " + AvailableMoves[0].Location + ", S: " + AvailableMoves[0].StepsFromGoal);
            ChosenDirection = AvailableMoves[0].Direction;
            PathNode node = new PathNode((Direction)((uint)ChosenDirection ^ 3), AvailableMoves[0].Location, endPoint);

            List<PathNode> nodes = new List<PathNode>();

            while ((nodes = node.GetNextNode(endPoint, AvailableMoves[0].StepsFromGoal)) == null)
            {
                if (AvailableMoves.Count <= 1)
                {
                    Console.WriteLine("Going up a level : No moves");
                    return null;
                }
                AvailableMoves.RemoveAt(0);
                Console.WriteLine(AvailableMoves[0].Direction);
                ChosenDirection = AvailableMoves[0].Direction;
                node = new PathNode((Direction)((uint)ChosenDirection ^ 3), AvailableMoves[0].Location, endPoint);

            }


            nodes.Add(node);
            return nodes;

        }
    }
}
