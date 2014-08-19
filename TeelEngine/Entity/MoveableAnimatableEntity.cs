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

        public override void Move(Direction direction)
        {
            if (direction == Direction.North) CurrentAnimation = "MOVE_UP";
            if (direction == Direction.East) CurrentAnimation = "MOVE_RIGHT";
            if (direction == Direction.South) CurrentAnimation = "MOVE_DOWN";
            if (direction == Direction.West) CurrentAnimation = "MOVE_LEFT";

            base.Move(direction);
        }
    }
}
