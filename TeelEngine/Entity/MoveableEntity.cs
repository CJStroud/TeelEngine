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
        public Vector2 Offset { get; set; }
        public Point NewLocation { get; set; }
        public Direction MoveDirection { get; set; }

        public void Move(Direction direction)
        {
            
        }

        public override void Update(Level.Level level, GameTime gameTime)
        {
            if (Location != NewLocation)
            {
                int diffX = NewLocation.X - entity.Location.X;
                int diffY = NewLocation.Y - entity.Location.Y;

                int signX = Math.Sign(diffX);
                int signY = Math.Sign(diffY);


                Offset = new Vector2(Offset.X + (signX * entity.Speed), Offset.Y + (signY * entity.Speed));

                if (Offset.X >= 1 || Offset.Y >= 1 || Offset.X <= -1 || Offset.Y <= -1)
                {
                    Location = NewLocation;
                    Offset = new Vector2(0, 0);
                }
            }

            var texture = Texture as AnimatedTexture;
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            texture.NextFrame(elapsed);
            base.Update(level, gameTime);
        }
    }
}
