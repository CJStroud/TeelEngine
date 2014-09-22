using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TeelEngine.Pathing
{
    public class PathFinder
    {
        #region Constants

        private const int BaseMovementCost = 10;

        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly int _xMaxValue;
        private readonly int _xMinValue;
        private readonly int _yMaxValue;
        private readonly int _yMinValue;

        #endregion

        #region Private Globals

        private PathNode _startNode;
        private PathNode _targetNode;
        private PathNode _nodeInFocus;
        private Path _path;

        private PathNode[,] _nodeMap;

        private Queue<PathNode> _openList;
        private ISet<PathNode> _closedList;
        private List<Point> _hardCollisionLocations; // collision points that wont change, hardcoded into the map
        private List<Point> _softCollisionLocations; // collision points that are liable to change during runtime
        private List<Point> _allCollisionLocations; 

        private bool _pathComplete;

        #endregion

        #region Constructor

        /// <summary>
        /// The pathfinder constructor, this should be instialised at the same time as the map or level
        /// </summary>
        /// <param name="mapMapWidth">The map width in tiles</param>
        /// <param name="mapMapHeight">The map height in tiles</param>
        /// <param name="hardCollisionLocations">The collison locations on the map that will never change during the maps lifetime</param>
        public PathFinder(int mapMapWidth, int mapMapHeight, List<Point> hardCollisionLocations)
        {
            // ensures hardCollisionLocations will not be set as null
            _hardCollisionLocations = hardCollisionLocations ?? new List<Point>();
            _allCollisionLocations = _hardCollisionLocations;

            _mapWidth = mapMapWidth;
            _mapHeight = mapMapHeight;
            _xMaxValue = mapMapWidth - 1;
            _yMaxValue = mapMapHeight - 1;
            _xMinValue = 0;
            _yMinValue = 0;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Finds the shortest possible path between the start and on point
        /// </summary>
        /// <param name="start">The paths starting point as a grid location</param>
        /// <param name="end">The paths ending point as a grid location</param>
        /// <returns>A path object representing the shortest route. This will be null if there is no path</returns>
        public Path FindPath(Point start, Point end)
        {
            if (start == end) return null;
            if (!IsWithinMapBounds(start)) return null;
            if (!IsWithinMapBounds(end)) return null;

            // variable setup
            NullOldVariables();
            if (_softCollisionLocations!=null) _allCollisionLocations.AddRange(_softCollisionLocations);

            // node / node map setup
            GenerateNodeMap();
            _startNode = _nodeMap[start.X, start.Y];
            _targetNode = _nodeMap[end.X, end.Y];

            if (IsACollisionPoint(_targetNode.Location)) return null;

            CalculateEstimateValues();
            _nodeInFocus = _startNode;
            _nodeInFocus = SetAdjacentNodes(_nodeInFocus);
            AddToOpenList(_nodeInFocus);

            // main pathfinding loop
            while (_path == null)
            {
                Update();
            }

            // reset collision locations
            _softCollisionLocations = null;
            _allCollisionLocations = _hardCollisionLocations;
            return _path;

        }

        /// <summary>
        /// Finds the shortest possible path between the start and on point
        /// </summary>
        /// <param name="start">The paths starting point as a grid location</param>
        /// <param name="end">The paths ending point as a grid location</param>
        /// <param name="softCollisionLocations">These are the collision points that may have changed each time a path is created</param>
        /// <returns>A path object representing the shortest route. This will be null if there is no path</returns>
        public Path FindPath(Point start, Point end, List<Point> softCollisionLocations)
        {
            _softCollisionLocations = softCollisionLocations ?? new List<Point>();
            return FindPath(start, end);
        }

        #endregion

        #region Private Methods

        private bool IsACollisionPoint(Point location)
        {
            return _allCollisionLocations.Contains(location);
        }

        /// <summary>
        /// Creates nodes to represent the map
        /// </summary>
        private void GenerateNodeMap()
        {
            var map = new PathNode[_mapWidth, _mapHeight];

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    var node = new PathNode(new Point(x, y));
                    node.ParentNode = null;
                    if (IsACollisionPoint(new Point(x, y)))
                        node.IsSolid = true;
                    map[x, y] = node;
                }

            }

            _nodeMap = map;
        }

        /// <summary>
        /// Checks to see whether the path has been completed or whether we need to keep trying
        /// </summary>
        private void Update()
        {
            if (_pathComplete)
            {
                TraceBackPath();
            }
            else
            {

                // Calculate the movement values for the adjacent nodes to the one we have in focus
                if (_nodeInFocus.NorthNode != null) CalculateNodeValue(_nodeInFocus, _nodeInFocus.NorthNode);
                if (_nodeInFocus.EastNode != null) CalculateNodeValue(_nodeInFocus, _nodeInFocus.EastNode);
                if (_nodeInFocus.SouthNode != null) CalculateNodeValue(_nodeInFocus, _nodeInFocus.SouthNode);
                if (_nodeInFocus.WestNode != null) CalculateNodeValue(_nodeInFocus, _nodeInFocus.WestNode);

                // Mark node as checked by adding it to the closed list / removing from open list
                // When the node is on the closed list it wont be checked again
                AddToClosedList(_nodeInFocus);
                RemoveNodeFromOpenList();
                _nodeInFocus = null;
                _nodeInFocus = GetBestNodeFromOpenList();
                _nodeInFocus = SetAdjacentNodes(_nodeInFocus);
            }
        }

        private PathNode SetAdjacentNodes(PathNode pathNode)
        {
            pathNode.NorthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y - 1));
            pathNode.EastNode = GetPathNodeAt(new Point(pathNode.Location.X + 1, pathNode.Location.Y));
            pathNode.SouthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y + 1));
            pathNode.WestNode = GetPathNodeAt(new Point(pathNode.Location.X - 1, pathNode.Location.Y));

            return pathNode;
        }

        /// <summary>
        /// This calculates the value of the movement between two nodes
        /// </summary>
        /// <param name="nodeInFocus">This is the node that the pathfinder is currently looking at</param>
        /// <param name="nodeToCheck">This is one of the nodes that is adjacent to the current node and is will be evaluated to see if it creates a shorter / faster path</param>
        private void CalculateNodeValue(PathNode nodeInFocus, PathNode nodeToCheck)
        {
            // We have found the target node, there is no need to continue
            if (nodeToCheck.Location == _targetNode.Location)
            {
                _pathComplete = true;
                _targetNode.ParentNode = nodeInFocus;
                return;
            }

            if (nodeToCheck.IsSolid) return;
            if (_closedList.Contains(nodeToCheck)) return;

            if (_openList.Contains(nodeToCheck))
            {
                // if moving to the node we are checking is cheaper than the node we are currently on then 
                // set the node we are looking ats parent to the node we are on
                int costToMove = nodeInFocus.EstimatedCost + BaseMovementCost;

                if (costToMove < nodeToCheck.EstimatedCost)
                {
                    nodeToCheck.ParentNode = nodeInFocus;
                    nodeToCheck.EstimatedCost = costToMove;
                    nodeToCheck.CalculateTotalCost();
                }

            }
            else // if we have found a node that is not on either the open or closed list
            {
                // set its parent to the node we are looking at & add it to the open list to be checked
                nodeToCheck.ParentNode = nodeInFocus;
                nodeToCheck.EstimatedCost = nodeInFocus.EstimatedCost + BaseMovementCost;
                nodeToCheck.CalculateTotalCost();
                AddToOpenList(nodeToCheck);
            }
        }

        /// <summary>
        /// Estimates the initial move values of every node using the number of steps it would take to get the target position, assuming there are no blockers / collisions
        /// </summary>
        private void CalculateEstimateValues()
        {
            foreach (PathNode pathNode in _nodeMap)
            {
                pathNode.EstimatedCost = BaseMovementCost * (Math.Abs(pathNode.Location.X - _targetNode.Location.X)
                                                           + Math.Abs(pathNode.Location.Y - _targetNode.Location.Y));
            }
        }

        /// <summary>
        /// Generates a list of PathNodes by cycling through the each pathnodes parent
        /// </summary>
        private void TraceBackPath()
        {

            // look at each nodes parent to create the best path
            _path = new Path();
            _path.Nodes = new Stack<PathNode>();
            PathNode node = _targetNode;
            do
            {
                if (node.ParentNode != null)
                {
                    _path.Nodes.Push(node);
                }
                node = node.ParentNode;
            } while (node != null);
        }    

        /// <summary>
        /// Clears out any old global variables
        /// </summary>
        private void NullOldVariables()
        {
            _path = null;
            _pathComplete = false;
            _openList = new Queue<PathNode>();
            _closedList = new HashSet<PathNode>();
            _nodeMap = null;
            _startNode = null;
            _targetNode = null;
        }

        /// <summary>
        /// Determines whether a point is within the bounds of the map
        /// </summary>
        /// <param name="location">The location to check</param>
        /// <returns>A bool to representing whether the point is inside the map bounds</returns>
        private bool IsWithinMapBounds(Point location)
        {
            return location.X <= _xMaxValue && location.X >= _xMinValue
                   && location.Y <= _yMaxValue && location.Y >= _yMinValue;
        }

        /// <summary>
        /// Returns the node that is at the location specified
        /// </summary>
        /// <param name="location">The x,y location of the node, represented as a point</param>
        /// <returns>The pathnode object to located at the x,y coords on the pathfinder map</returns>
        private PathNode GetPathNodeAt(Point location)
        {
            return IsWithinMapBounds(location) ? _nodeMap[location.X, location.Y] : null;
        }

        /// <summary>
        /// Gets the node with the smallest total cost from the nodes currently in the openlist
        /// </summary>
        /// <returns>The PathNode with the smallest TotalCost</returns>
        private PathNode GetBestNodeFromOpenList()
        {
            PathNode smallestValue = null;

            foreach (PathNode node in _openList)
            {
                if (smallestValue == null)
                    smallestValue = node;

                else if (smallestValue.TotalCost > node.TotalCost)
                    smallestValue = node;
            }

            return smallestValue;
        }

        /// <summary>
        /// Adds a node to the open list
        /// </summary>
        /// <param name="pathNode">The pathnode to add</param>
        private void AddToOpenList(PathNode pathNode)
        {
            _openList.Enqueue(pathNode);
        }

        /// <summary>
        /// Add a node to the closed list
        /// </summary>
        /// <param name="pathNode">The pathnode to add</param>
        private void AddToClosedList(PathNode pathNode)
        {
            _closedList.Add(pathNode);
        }

        /// <summary>
        /// Removes the first node that was added to the openlist
        /// </summary>
        private void RemoveNodeFromOpenList()
        {
            _openList.Dequeue();

        }

        #endregion

    }
}