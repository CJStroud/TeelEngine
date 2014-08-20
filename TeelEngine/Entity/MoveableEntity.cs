using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TeelEngine.Level.Interfaces;

namespace TeelEngine
{
    public class MoveableEntity : Entity, IMoveable
    {
        public float Speed { get; set; }
        public Vector2 NewLocation { get; set; }
        public Direction MoveDirection { get; set; }
        public bool IsMoving { get; set; }

        public Path Path { get; set; }

        public virtual void Move(Vector2 destination)
        {
            if (!IsMoving && destination != Location)
            {
                NewLocation = destination;
                IsMoving = true;
            }
        }

        public virtual void Move(Direction direction)
        {
            if(direction == Direction.North) Move(new Vector2(Location.X, Location.Y - 1));
            if(direction == Direction.East) Move(new Vector2(Location.X + 1, Location.Y));
            if(direction == Direction.South) Move(new Vector2(Location.X,  Location.Y + 1));
            if(direction == Direction.West) Move(new Vector2(Location.X - 1, Location.Y));
        }

        public override void Update(Level.Level level, GameTime gameTime)
        {
            if (IsMoving)
            {
                float diffX = NewLocation.X - Location.X;
                float diffY = NewLocation.Y - Location.Y;

                int signX = Math.Sign(diffX);
                int signY = Math.Sign(diffY);

                Offset = new Vector2(Offset.X + ((signX * Speed) * Math.Abs(diffX)), Offset.Y + ((signY * Speed) * Math.Abs(diffY)));

                if (Math.Abs((Location.X + Offset.X) - NewLocation.X) < Speed && Math.Abs((Location.Y + Offset.Y) - NewLocation.Y) < Speed)
                {
                    Location = NewLocation;
                    Offset = new Vector2(0, 0);
                    IsMoving = false;
                }
            }
            else
            {
                if (Path != null)
                {
                    Move(Path.GetNextLocation());
                    if (Path.ReachedEnd) Path = null;
                }
            }

            base.Update(level, gameTime);
        }
    }
}
