using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using Microsoft.Xna.Framework;
using TeelEngine;
using Path = TeelEngine.Path;

namespace aStarPathfinding
{
    public class PathFinder
    {

        #region consts

        public const int BaseMovementCost = 10;

        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly int _xMaxValue;
        private readonly int _xMinValue;
        private readonly int _yMaxValue;
        private readonly int _yMinValue;

        #endregion

        #region private variables

        private PathNode _startNode;
        private PathNode _targetNode;
        private PathNode _nodeToCheck;
        private Path _path;

        private PathNode[,] _nodeMap;

        private Queue<PathNode> _openList;
        private ISet<PathNode> _closedList;
        private readonly List<Vector2> _permCollisionLocations; // collision points that wont change, hardcoded into the map
        private List<Vector2> _allCollisionLocations; 

        private bool _pathComplete;

        #endregion

        #region constructors

        public PathFinder(int mapMapWidth, int mapMapHeight, List<Vector2> permCollisionLocations)
        {
            _mapWidth = mapMapWidth;
            _mapHeight = mapMapHeight;
            _xMaxValue = mapMapWidth - 1;
            _yMaxValue = mapMapHeight - 1;
            _xMinValue = 0;
            _yMinValue = 0;

            _permCollisionLocations = permCollisionLocations;
        }

        #endregion

        #region public methods

        public Path FindPath(Point start, Point end)
        {
            if (start == end) return null;
            if (!IsWithinMapBounds(start)) return null;
            if (!IsWithinMapBounds(end)) return null;

            NullOldVariables();

            // todo: pass through the variable collisions and merge with perm collision
            // note variable collisions are things such as moving entities
            _allCollisionLocations = _permCollisionLocations;

            GenerateNodeMap();
            _startNode = _nodeMap[start.X, start.Y];
            _targetNode = _nodeMap[end.X, end.Y];

            CalculateEstimateValues();

            _nodeToCheck = _startNode;
            _nodeToCheck = SetAdjacentNodes(_nodeToCheck);

            while (_path == null)
            {
                Update();
            }

            return _path;

        }

        #endregion

        #region main private methods

        private void GenerateNodeMap()
        {
            var map = new PathNode[_mapWidth, _mapHeight];

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    var node = new PathNode(new Point(x, y));
                    node.ParentNode = null;
                    if (_allCollisionLocations.Contains(new Vector2(x, y)))
                        node.IsSolid = true;
                    map[x, y] = node;
                }

            }

            _nodeMap = map;
        }

        private void Update()
        {
            if (_pathComplete)
            {
                TraceBackPath();
            }
            else
            {
                if (_pathComplete) return;

                if (_nodeToCheck.NorthNode != null) CalculateNodeValue(_nodeToCheck, _nodeToCheck.NorthNode);
                if (_nodeToCheck.EastNode != null) CalculateNodeValue(_nodeToCheck, _nodeToCheck.EastNode);
                if (_nodeToCheck.SouthNode != null) CalculateNodeValue(_nodeToCheck, _nodeToCheck.SouthNode);
                if (_nodeToCheck.WestNode != null) CalculateNodeValue(_nodeToCheck, _nodeToCheck.WestNode);

                if (_pathComplete) return;

                AddToClosedList(_nodeToCheck);
                RemoveFromOpenList();
                _nodeToCheck = null;
                _nodeToCheck = GetBestNodeFromOpenList();
                _nodeToCheck = SetAdjacentNodes(_nodeToCheck);
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

        private void CalculateNodeValue(PathNode currentNode, PathNode nodeToCheck)
        {
            if (currentNode.Location == nodeToCheck.Location)
                throw new ArgumentException("The current node and the target node cannot be the same!");

            if (nodeToCheck.Location == _targetNode.Location)
            {
                _pathComplete = true;
                _targetNode.ParentNode = currentNode;
                return;
            }

            if (nodeToCheck.IsSolid) return;
            if (_closedList.Contains(nodeToCheck)) return;

            if (_openList.Contains(nodeToCheck))
            {

                int newGCost = currentNode.EstimatedCost + BaseMovementCost;

                // if the move cost if lower to the node we are checking then we want to change the nodes parent to this so it have the lowest move cost
                if (newGCost < nodeToCheck.EstimatedCost)
                {
                    nodeToCheck.ParentNode = currentNode;
                    nodeToCheck.EstimatedCost = newGCost;
                    nodeToCheck.CalculateTotalCost();
                }

            }
            else // if the node we are checking is not on the openlist or closed list, then add to openlist for checking
            {
                nodeToCheck.ParentNode = currentNode;
                nodeToCheck.EstimatedCost = currentNode.EstimatedCost + BaseMovementCost;
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

        #endregion

        #region helper private methods

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

        private void AddToOpenList(PathNode pathNode)
        {
            _openList.Enqueue(pathNode);
        }

        private void AddToClosedList(PathNode pathNode)
        {
            _closedList.Add(pathNode);
        }

        private void RemoveFromOpenList()
        {

            _openList.Dequeue();

        }

        #endregion

    }
}