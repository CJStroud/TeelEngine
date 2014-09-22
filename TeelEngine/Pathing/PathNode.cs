using System;
using Microsoft.Xna.Framework;

namespace TeelEngine.Pathing
{
    /// <summary>
    /// Represents a node of the map and holds data relating to the path
    /// </summary>
    public class PathNode
    {

        #region Properties

        /// <summary>
        /// This node that comes before this one is the path, if this is the starting node it will be null
        /// </summary>
        public PathNode ParentNode
        {
            get { return _parentNode; }
            set
            {
                _parentNode = value;
            }
        }

        /// <summary>
        /// The grid reference location of this node
        /// </summary>
        public Point Location { get; private set; }

        public PathNode NorthNode { get; set; }
        public PathNode EastNode { get; set; }
        public PathNode SouthNode { get; set; }
        public PathNode WestNode { get; set; }

        public int EstimatedCost { get; set; }
        public int MovementCost { get; set; }
        public int TotalCost { get; set; }

        /// <summary>
        /// Is the node able to be part of the path
        /// </summary>
        public bool IsSolid { get; set; }

        #endregion

        #region Private Globals

        private PathNode _parentNode;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new PathNode
        /// </summary>
        /// <param name="location">The point on the map that this PathNode represents</param>
        public PathNode(Point location)
        {
            Location = location;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates the total cost of moving through the node, using the estimated cost and movement cost
        /// </summary>
        public void CalculateTotalCost()
        {
            TotalCost = EstimatedCost + MovementCost;
        }

        #endregion

    }
}
