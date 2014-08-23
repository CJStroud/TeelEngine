using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TeelEngine.Render;

namespace TeelEngine
{
    public class MoveableAnimatableEntity : MoveableEntity, IAnimatable
    {
        public Dictionary<string, int> Animations { get; set; }
        public string CurrentAnimation { get; set; }

        public override void Update(Level.Level level, GameTime gameTime)
        {
            if (!IsMoving) CurrentAnimation = null;
            var texture = Texture as AnimatedTexture;
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            texture.NextFrame(elapsed);

            base.Update(level, gameTime);
        }

        public override void Move(Vector2 destination)
        {
            if (IsMoving || destination == Location) return;

            if (Location.Y > destination.Y) CurrentAnimation = GetAnimationByDirection(Direction.North);
            if (Location.X < destination.X) CurrentAnimation = GetAnimationByDirection(Direction.East);
            if (destination.Y > Location.Y) CurrentAnimation = GetAnimationByDirection(Direction.South);
            if (destination.X < Location.X) CurrentAnimation = GetAnimationByDirection(Direction.West);

            base.Move(destination);
        }

        public override void Move(Direction direction)
        {
            if (IsMoving || direction != Direction.None) return;
            base.Move(direction);
        }

        public string GetAnimationByDirection(Direction direction)
        {
            if (direction == Direction.North) return "MOVE_UP";
            if (direction == Direction.East) return "MOVE_RIGHT";
            if (direction == Direction.South) return "MOVE_DOWN";
            if (direction == Direction.West) return "MOVE_LEFT";

            return null;
        }
    }
}
