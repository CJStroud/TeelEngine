using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using TeelEngine;

namespace UnitTests
{

    public enum Direction
    {
        North = 0,
        East,
        West,
        South,
        None
    }

    public class PathTests
    {
        #region Create Path
        [Test]
        public void CreateOneStepPath()
        {
            Point start = new Point(0, 0);
            Point end = new Point(0, 1);
            Path path = Path.Create(start, end);

            Assert.AreEqual(Direction.South, path.GetDirectionAt(0));
        }

        [Test]
        public void CreatePath_SouthSouth()
        {
            Point start = new Point(0, 0);
            Point end = new Point(0, 2);
            Path path = Path.Create(start, end);

            Assert.AreEqual(Direction.South, path.GetDirectionAt(0));
            Assert.AreEqual(Direction.South, path.GetDirectionAt(1));
        }

        [Test]
        public void CreatePath_West()
        {
            Point start = new Point(2, 0);
            Point end = new Point(1, 0);
            Path path = Path.Create(start, end);

            Assert.AreEqual(Direction.West, path.GetDirectionAt(0));
        }

        [Test]
        public void CreatSouthEastSouthEast()
        {
            Point start = new Point(0, 0);
            Point end = new Point(2, 2);
            Path path = Path.Create(start, end);

            List<Direction> expectedDirections = new List<Direction>
            {
                Direction.South, Direction.East, Direction.South, Direction.East
            };

            Assert.AreEqual(expectedDirections, path.Directions);
        }
        #endregion

        #region CalcBestDirection
        [Test]
        public void CalcBestMove_ReturnsWest_WhenCurrentLocationIs55AndEndLocationIs45()
        {
            Path path = Path.Create(new Point(5, 5), new Point(4, 5));

            Direction actualDirection = path.GetDirectionAt(0);

            Direction expectedDirection = Direction.West;
            Assert.AreEqual(expectedDirection, actualDirection);
        }

        [Test]
        public void CalcBestMove_ReturnsEast_WhenCurrentLocationIs45AndEndLocationIs55()
        {
            Path path = Path.Create(new Point(4, 5), new Point(5, 5));

            Direction actualDirection = path.GetDirectionAt(0);

            Direction expectedDirection = Direction.East;
            Assert.AreEqual(expectedDirection, actualDirection);
        }

        [Test]
        public void CalcBestMove_ReturnsSouth_WhenCurrentLocationIs11AndEndLocationIs25()
        {
            Path path = Path.Create(new Point(1, 1), new Point(2, 5));

            Direction actualDirection = path.GetDirectionAt(0);

            Direction expectedDirection = Direction.South;
            Assert.AreEqual(expectedDirection, actualDirection);
        }

        [Test]
        public void CalcBestMove_ReturnsEast_WhenCurrentLocationIs11AndEndLocationIs52()
        {
            Path path = Path.Create(new Point(1, 1), new Point(5, 2));

            Direction actualDirection = path.GetDirectionAt(0);

            Direction expectedDirection = Direction.East;
            Assert.AreEqual(expectedDirection, actualDirection);
        }

        #endregion

    }

    public class Path
    {
        public List<Direction> Directions
        {
            get
            {
                List<Direction> directions = Moves.Select(move => move.Direction).ToList();
                return directions;
            }
        }

        public List<Move> Moves { get; set; }

        private Point _currentLocation;
        private Point _endLocation;

        public static Path Create(Point startPoint, Point endPoint)
        {
            return new Path(startPoint , endPoint);
        }

        public Path(Point startPoint, Point endPoint)
        {
            _currentLocation = startPoint;
            _endLocation = endPoint;
            Moves = new List<Move>();

            if (_currentLocation == endPoint)
            {
                Console.WriteLine("return empty path");
            }
            AddMove(new List<Direction>{Direction.None});

            while (_currentLocation != _endLocation)
            {
                Direction lastMoveDirection = Moves.Last().Direction;
                if (!AddMove(new List<Direction>{OppositeDirectionOf(lastMoveDirection)}))
                {
                    List<Direction> badDirections = Moves.Last().UnusableDirections;
                    badDirections.Add(Moves.Last().Direction);
                    Moves.RemoveAt(Moves.Count - 1);
                    AddMove(badDirections);
                }
            }

        }

        private bool AddMove(List<Direction> previousDirectionsTried)
        {
            Move move = CalcBestDirection(previousDirectionsTried); // collision points will go through here too
            if (move == null) return false;
            _currentLocation = move.Location;
            Moves.Add(move);
            return true;
        }

        public Direction GetDirectionAt(int index)
        {
            return Moves.Count > index ? Moves[index].Direction : Direction.None;
        }

        public Move CalcBestDirection(List<Direction> directionsNotValid)
        {
            List<Move> moves = new List<Move>
            {
                new Move(new Point(_currentLocation.X + 1, _currentLocation.Y), _endLocation, Direction.East),
                new Move(new Point(_currentLocation.X - 1, _currentLocation.Y), _endLocation, Direction.West),
                new Move(new Point(_currentLocation.X, _currentLocation.Y + 1), _endLocation, Direction.South),
                new Move(new Point(_currentLocation.X, _currentLocation.Y - 1), _endLocation, Direction.North),
            };

            foreach (var move in moves)
            {
                if (CollisionDetection.CollisionAtPoint(move.Location))
                {
                    directionsNotValid.Add(move.Direction);
                }
            }

            if (directionsNotValid != null)
            {
                foreach (var direction in directionsNotValid)
                {
                    moves.RemoveAll(m => m.Direction == direction);
                }
            }
            if (moves.Count < 1) return null;


            // order list depending on whether the number of steps on x / y axis is bigger, this gives a more natural movement towards the goal
            int xSteps = Math.Abs(_currentLocation.X - _endLocation.X);
            int ySteps = Math.Abs(_currentLocation.Y - _endLocation.Y);

            moves = xSteps > ySteps
                ? new List<Move>(moves.OrderBy(m => m.XSteps)) : new List<Move>(moves.OrderBy(m => m.YSteps));

            return moves.First();
        }

        private Direction OppositeDirectionOf(Direction direction)
        {
            if (direction == Direction.North) return Direction.South;
            if (direction == Direction.South) return Direction.North;
            if (direction == Direction.West) return Direction.East;
            if (direction == Direction.East) return Direction.West;

            return Direction.None;
        }
    }
}
