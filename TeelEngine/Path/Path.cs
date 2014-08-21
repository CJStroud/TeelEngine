using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class Path
    {
        #region properties

        /// <summary>
        /// Stack of PathNodes that make up the path
        /// </summary>
        public Stack<PathNode> Nodes { get; set; }
        public bool ReachedEnd {
            get { return Nodes.Count == 0; }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Returns the next nodes location as a vector and removes that node from the path
        /// </summary>
        /// <returns>Location of the next node</returns>
        public Vector2 GetNextLocation()
        {
            if (Nodes.Count == 0) return Vector2.Zero;

            PathNode node = Nodes.Pop();
            return new Vector2(node.Location.X, node.Location.Y);
        }

        #endregion

    }
}
