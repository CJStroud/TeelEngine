using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class Path
    {
        public List<PathNode> PathNodes { get; set; }

        public static Path CreatePath(Point startPoint, Point endPoint)
        {
            PathNode initialNode = new PathNode(Direction.None, startPoint, endPoint);
            Path path = new Path();
            path.SetPath(initialNode.GetNextNode(endPoint, int.MaxValue));
            if(path.PathNodes.Count > 1)path.PathNodes.RemoveAt(0);
            return path;
        }


        public void SetPath(List<PathNode> path)
        {
            PathNodes = path;
        }

        public Direction GetNextDirection()
        {
            Direction direction = PathNodes.Last().ChosenDirection;
            PathNodes.Remove(PathNodes.Last());
            return direction;
        }
    }
}