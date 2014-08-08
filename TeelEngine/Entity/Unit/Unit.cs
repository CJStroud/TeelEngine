using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TeelEngine
{
    public class Unit : IEntity
    {
        public string TextureName { get; set; }
        public int Index { get; set; }
        public Vector2 Location { get; set; }
        [ContentSerializerIgnore]
        public bool IsMoving = false;

        [ContentSerializerIgnore]
        public Vector2 ScreenPosition { get; set; }

        public float Speed { get; set; }

        [ContentSerializerIgnore]
        public Vector2 Velocity;

        [ContentSerializerIgnore]
        public Direction Direction;

        [ContentSerializerIgnore] 
        private Path _path;

        public Unit()
        {
            TextureName = "";
            Location = new Vector2(0, 0);
            Index = 0;
        }

        public Unit(string textureName, Vector2 location, int index)
        {
            TextureName = textureName;
            Location = location;
            Index = index;
        }

        public void Interact()
        {

        }

        public void Render(SpriteBatch spriteBatch)
        {
            Render(spriteBatch, ScreenPosition);
        }

        public void Render(SpriteBatch spriteBatch, Vector2 screenLocation)
        {
            ITexture texture = Globals.TextureController.Get(TextureName);
            if (texture == null) return;
            var animatedTexture = texture as AnimatedTexture;
            if (animatedTexture != null)
            {
                animatedTexture.Render(spriteBatch, screenLocation);
            }
            else
            {
                var sprite = texture as SpriteTexture;
                if (sprite != null)
                {
                    sprite.Render(spriteBatch, Index, screenLocation);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            ITexture texture = Globals.TextureController.Get(TextureName);
            var animatedTexture = (AnimatedTexture)texture;

            if (IsMoving)
            {
                animatedTexture.Paused = false;
                UpdateScreenPosition();
            }
            else
            {
                animatedTexture.Paused = true;
                if (_path != null && _path.Nodes != null && _path.Nodes.Count > 0)
                {
                    Direction direction = _path.GetNextDirection();
                    if (direction != Direction.None) Move(direction);
                }
            }
        }

        private void UpdateScreenPosition()
        {
            var nextMoveLocation = new Vector2((ScreenPosition.X + Velocity.X), (ScreenPosition.Y + Velocity.Y));
            var targetEndLocation = new Vector2(Location.X * Globals.TileSize, Location.Y * Globals.TileSize);

            IsMoving = CheckStillMoving(nextMoveLocation, targetEndLocation);
            ScreenPosition = IsMoving ? nextMoveLocation : targetEndLocation;
        }

        private bool CheckStillMoving(Vector2 nextMoveLocation, Vector2 targetEndLocation)
        {
            if (Direction == Direction.North && nextMoveLocation.Y <= targetEndLocation.Y) return false;
            if (Direction == Direction.South && nextMoveLocation.Y >= targetEndLocation.Y) return false;
            if (Direction == Direction.East && nextMoveLocation.X >= targetEndLocation.X) return false;
            if (Direction == Direction.West && nextMoveLocation.X <= targetEndLocation.X) return false;

            return true;
        }


        public void Initialize()
        {
            ScreenPosition = new Vector2(Location.X * Globals.TileSize, Location.Y * Globals.TileSize);
        }


        public void Move(Direction direction)
        {
            if (IsMoving) return;
            Direction = direction;
            Location = GetNewLocation(direction);

            Velocity = Vector2.Zero;
            if (GetAnimatedTexture() != null)
            {
                GetAnimatedTexture().Row = (int)Direction;
                Velocity = GetVelocityForDirection(direction);
                IsMoving = true;
            }

        }

        public void Move(Path path)
        {
            _path = path;
        }

        private Vector2 GetVelocityForDirection(Direction direction)
        {
            if (direction == Direction.North) return new Vector2(0, -Speed);
            if (direction == Direction.South) return new Vector2(0, Speed);
            if (direction == Direction.East) return new Vector2(Speed, 0);
            if (direction == Direction.West) return new Vector2(-Speed, 0);

            return Vector2.Zero;
        }

        private Vector2 GetNewLocation(Direction direction)
        {
            if (direction == Direction.North) return new Vector2(Location.X, Location.Y - 1);
            if (direction == Direction.South) return new Vector2(Location.X, Location.Y + 1);
            if (direction == Direction.East) return new Vector2(Location.X + 1, Location.Y);
            if (direction == Direction.West) return new Vector2(Location.X - 1, Location.Y);

            return Vector2.Zero;
        }

        public void MoveUp()
        {
            Move(Direction.North);
        }

        public void MoveDown()
        {
            Move(Direction.South);
        }

        public void MoveRight()
        {
            Move(Direction.East);
        }

        public void MoveLeft()
        {
            Move(Direction.West);
        }



        public AnimatedTexture GetAnimatedTexture()
        {
            ITexture texture = Globals.TextureController.Get(TextureName);
            var animatedTexture = (AnimatedTexture)texture;
            return animatedTexture;
        }

        public SpriteTexture GetSpriteTexture()
        {
            ITexture texture = Globals.TextureController.Get(TextureName);
            var animatedTexture = (SpriteTexture)texture;
            return animatedTexture;
        }
    }
}
