using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.Xna.Framework;
using TeelEngine;
using Path = TeelEngine.Path;

namespace aStarPathfinding
{
    public class PathFinder
    {
        private List<PathNode> OpenList = new List<PathNode>(); // The open list is for nodes to be checked
        private List<PathNode> ClosedList = new List<PathNode>(); // The closed list for nodes that have been checked


        private bool targetFound = false;

        public PathNode CheckingNode = null;

        public PathNode[,] Map { get; set; }

        public Path Path { get; set; }
        public PathNode StartNode = null;
        public PathNode TargetNode = null;

        public const int BaseMovementCost = 10;

        public PathFinder(int mapWidth, int mapHeight, List<Vector2> collisions, Point start, Point end)
        {
            Map = GenerateNodes(mapWidth, mapHeight, collisions);
            StartNode = Map[start.X, start.Y];
            TargetNode = Map[end.X, end.Y];
        }

        private PathNode[,] GenerateNodes(int width, int height, List<Vector2> collisions)
        {
            var map = new PathNode[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var node = new PathNode(new Point(x, y));

                    if (collisions.Contains(new Vector2(x, y)))
                        node.IsSolid = true;

                    map[x, y] = node;

                }

            }

            return map;
        }

        public void Create()
        {
            SetAdjacentNodes();

            CalculateEstimateValues();
            CheckingNode = StartNode;

            while (Path == null)
            {
                Update();
            }
        }

        private void Update()
        {
            if (targetFound)
            {
                TraceBackPath();
            }
            else
            {
                FindPath();
            }
        }

        public void FindPath()
        {
            if (targetFound == false)
            {
                if (CheckingNode.NorthNode != null)
                    CalculateNodeValue(CheckingNode, CheckingNode.NorthNode);

                if (CheckingNode.EastNode != null)
                    CalculateNodeValue(CheckingNode, CheckingNode.EastNode);
            
                if (CheckingNode.SouthNode != null)
                    CalculateNodeValue(CheckingNode, CheckingNode.SouthNode);

                if (CheckingNode.WestNode != null)
                    CalculateNodeValue(CheckingNode, CheckingNode.WestNode);

                if (targetFound == false)
                {
                    AddToClosedList(CheckingNode);
                    RemoveFromOpenList(CheckingNode);

                    CheckingNode = GetSmallestValueNode();
                }
            }
        }

        public void SetAdjacentNodes()
        {
            foreach (var pathNode in Map)
            {
                pathNode.NorthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y - 1));
                pathNode.EastNode = GetPathNodeAt(new Point(pathNode.Location.X + 1, pathNode.Location.Y));
                pathNode.SouthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y + 1));
                pathNode.WestNode = GetPathNodeAt(new Point(pathNode.Location.X - 1, pathNode.Location.Y));
            }
        }

        private PathNode GetPathNodeAt(Point location)
        {
            PathNode node;
            try
            {
                node = Map[location.X, location.Y];
            }
            catch (IndexOutOfRangeException)
            {
                node = null;
            }

            return node;
        }

        public void CalculateEstimateValues()
        {
            foreach (PathNode pathNode in Map)
            {
                pathNode.EstimatedCost = 10*(Math.Abs(pathNode.Location.X - TargetNode.Location.X)
                                             + Math.Abs(pathNode.Location.Y - TargetNode.Location.Y));
            }
        }

        public void CalculateNodeValue(PathNode currentNode, PathNode nodeToCheck)
        {
            if (currentNode == nodeToCheck)
                throw new ArgumentException("The current node and the target node cannot be the same!");

            if (nodeToCheck.Location == TargetNode.Location)
            {
                // We have reached our target!
                TargetNode.ParentNode = currentNode;
                targetFound = true;
                return;
            }

            // Don't bother checking because we can never move there
            if (nodeToCheck.IsSolid)
                return;

            // Don't check if it is on the closed list
            if (ClosedList.Contains(nodeToCheck))
                return;

            if (OpenList.Contains(nodeToCheck))
            {
                int newGCost = currentNode.EstimatedCost + BaseMovementCost;


                if (newGCost < nodeToCheck.EstimatedCost)
                {
                    nodeToCheck.ParentNode = currentNode;
                    nodeToCheck.EstimatedCost = newGCost;
                    nodeToCheck.CalculateTotalCost();
                }
            
            }
            else
            {
                nodeToCheck.ParentNode = currentNode;
                nodeToCheck.EstimatedCost = currentNode.EstimatedCost + BaseMovementCost;
                nodeToCheck.CalculateTotalCost();
                AddToOpenList(nodeToCheck);
            }
        }

        private void AddToOpenList(PathNode pathNode)
        {
            OpenList.Add(pathNode);
        }

        private void RemoveFromOpenList(PathNode pathNode)
        {
            if (OpenList.Contains(pathNode))
                OpenList.Remove(pathNode);
        }

        private void AddToClosedList(PathNode pathNode)
        {
            ClosedList.Add(pathNode);
        }

        private PathNode GetSmallestValueNode()
        {
            PathNode smallestValue = null;

            foreach (PathNode node in OpenList)
            {
                if (smallestValue == null)
                    smallestValue = node;

                else if (smallestValue.TotalCost > node.TotalCost)
                    smallestValue = node;
            }

                return smallestValue;
        }

        private void TraceBackPath()
        {
            Path = new Path();
            Path.Nodes = new List<PathNode>();
            PathNode node = TargetNode;  
            do
            {

                if (node.ParentNode != null)
                {
                    node.Direction = CalculateDirection(node, node.ParentNode);
                    Path.Nodes.Insert(0, node);
                }
                node = node.ParentNode;
            } while (node != null);
        }

        private Direction CalculateDirection(PathNode moveToNode, PathNode moveFromNode)
        {
            if (moveFromNode.Location.X > moveToNode.Location.X) return Direction.West;
            if (moveFromNode.Location.X < moveToNode.Location.X) return Direction.East;
            if (moveFromNode.Location.Y > moveToNode.Location.Y) return Direction.North;
            if (moveFromNode.Location.Y < moveToNode.Location.Y) return Direction.South;

            return Direction.None;


        }
    
    }
}