using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeelEngine.Pathing
{
    public class Path
    {
        #region Properties

        /// <summary>
        /// Returns a value to represent whether the path has any more steps
        /// </summary>
        public bool HasReachedEnd {
            get { return Nodes.Count == 0; }
        }

        #endregion

        #region Private Globals

        public Stack<PathNode> Nodes { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the next nodes location as a vector and removes that node from the path
        /// </summary>
        /// <returns>Location of the next node</returns>
        public Vector2 GetNextLocation()
        {
            if (HasReachedEnd) return Vector2.Zero;

            PathNode node = Nodes.Pop();
            return new Vector2(node.Location.X, node.Location.Y);
        }

        #endregion
    }
}
