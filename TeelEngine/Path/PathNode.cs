using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class PathNode
    {
        public PathNode ParentNode = null; // The node that is before in the path

        // The nodes around this node 
        // These will be null if there is no node, or a node that cannot be passed through
        public PathNode NorthNode { get; set; }
        public PathNode EastNode { get; set; }
        public PathNode SouthNode { get; set; }
        public PathNode WestNode { get; set; }

        public int EstimatedCost { get; set; }      // g
        public int MovementCost { get; set; }       // h
        public int TotalCost { get; set; }          // f (g + h)

        public bool IsSolid { get; set; }
        public Point Location { get; private set; }
        public Direction Direction { get; set; }

        public PathNode(Point location)
        {
            Location = location;
        }

        /// <summary>
        /// Calculates the total cost of moving through the node
        /// </summary>
        public void CalculateTotalCost()
        {
            TotalCost = EstimatedCost + MovementCost;
        }
    }
}
