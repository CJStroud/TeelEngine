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
        private List<PathNode> OpenList = new List<PathNode>(); // The open list is for nodes to be checked
        private List<PathNode> ClosedList = new List<PathNode>(); // The closed list for nodes that have been checked


        private bool targetFound = false;

        public PathNode CheckingNode = null;

        public PathNode[,] TempMap { get; set; }
        public PathNode[,] Map { get; set; }

        public Path Path { get; set; }
        public PathNode StartNode = null;
        public PathNode TargetNode = null;

        public const int BaseMovementCost = 10;

        private int width;
        private int height;
        private List<Vector2> collisions; 

        public PathFinder(int mapWidth, int mapHeight, List<Vector2> collisions)
        {
            width = mapWidth;
            height = mapHeight;
            this.collisions = collisions;

            //Map = GenerateNodes(mapWidth, mapHeight, collisions);
        }

        private PathNode[,] GenerateNodes(int width, int height, List<Vector2> collisions)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var map = new PathNode[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var node = new PathNode(new Point(x, y));
                    node.ParentNode = null;
                    if (collisions.Contains(new Vector2(x, y)))
                        node.IsSolid = true;

                    map[x, y] = node;

                }

            }
            watch.Stop();
            Console.WriteLine("Generating Nodes - " + watch.ElapsedMilliseconds);
            return map;
        }

        public void Create(Point start, Point end)
        {
            Path = null;
            targetFound = false;
            OpenList = new List<PathNode>();
            ClosedList = new List<PathNode>();
            Map = null;
            Map = GenerateNodes(width, height, collisions);

            StartNode = null;
            TargetNode = null;

            StartNode = Map[start.X, start.Y];
            StartNode.ParentNode = null;
            StartNode.Start = true;
            TargetNode = Map[end.X, end.Y];

            CalculateEstimateValues();
            CheckingNode = null;
            CheckingNode = StartNode;
            CheckingNode = SetAdjacentNodes(CheckingNode);

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
                if (CheckingNode.NorthNode != null && !targetFound)
                    CalculateNodeValue(CheckingNode, CheckingNode.NorthNode);

                if (CheckingNode.EastNode != null && !targetFound)
                    CalculateNodeValue(CheckingNode, CheckingNode.EastNode);

                if (CheckingNode.SouthNode != null && !targetFound)
                    CalculateNodeValue(CheckingNode, CheckingNode.SouthNode);

                if (CheckingNode.WestNode != null && !targetFound)
                    CalculateNodeValue(CheckingNode, CheckingNode.WestNode);

                if (targetFound == false)
                {
                    AddToClosedList(CheckingNode);
                    RemoveFromOpenList(CheckingNode);
                    CheckingNode = null;
                    CheckingNode = GetSmallestValueNode();
                    CheckingNode = SetAdjacentNodes(CheckingNode);
                }
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

        public PathNode SetAdjacentNodes(PathNode pathNode)
        {
            pathNode.NorthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y - 1));
            pathNode.EastNode = GetPathNodeAt(new Point(pathNode.Location.X + 1, pathNode.Location.Y));
            pathNode.SouthNode = GetPathNodeAt(new Point(pathNode.Location.X, pathNode.Location.Y + 1));
            pathNode.WestNode = GetPathNodeAt(new Point(pathNode.Location.X - 1, pathNode.Location.Y));

            return pathNode;
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
            if (currentNode.Location == nodeToCheck.Location)
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

                    if (node.ParentNode.ParentNode != null && node.Location == node.ParentNode.ParentNode.Location)
                    {
                        Console.WriteLine("IT'S FUCKED");
                    }
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