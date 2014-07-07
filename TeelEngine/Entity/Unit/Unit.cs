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

        public Unit()
        {
            TextureName = "";
            Location = new Vector2(0,0);
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
                if (Direction == Direction.East)
                {
                    if (ScreenPosition.X + Velocity.X >= Location.X*((float) Globals.TileSize))
                    {
                        ScreenPosition = new Vector2(Location.X*((float) Globals.TileSize), ScreenPosition.Y);
                        IsMoving = false;
                    }
                    else
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X + Velocity.X, ScreenPosition.Y);
                    }
                }

                if (Direction == Direction.South)
                {
                    if (ScreenPosition.Y + Velocity.Y >= Location.Y*((float) Globals.TileSize))
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X, Location.Y*((float) Globals.TileSize));
                        IsMoving = false;
                    }
                    else
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X, ScreenPosition.Y + Velocity.Y);
                    }
                }

                if (Direction == Direction.West)
                {
                    if (ScreenPosition.X + Velocity.X <= Location.X * ((float)Globals.TileSize))
                    {
                        ScreenPosition = new Vector2(Location.X * ((float)Globals.TileSize), ScreenPosition.Y);
                        IsMoving = false;
                    }
                    else
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X + Velocity.X, ScreenPosition.Y);
                    }
                }

                if (Direction == Direction.North)
                {
                    if (ScreenPosition.Y + Velocity.Y <= Location.Y * ((float)Globals.TileSize))
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X, Location.Y * ((float)Globals.TileSize));
                        IsMoving = false;
                    }
                    else
                    {
                        ScreenPosition = new Vector2(ScreenPosition.X, ScreenPosition.Y + Velocity.Y);
                    }
                }
            }
            else
            {
                animatedTexture.Paused = true;
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    MoveRight();
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    MoveLeft();
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    //MoveDown();
                }
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    MoveUp();
                }
            }
        }

        public void Initialize()
        {
            ScreenPosition = new Vector2(Location.X*Globals.TileSize, Location.Y*Globals.TileSize);
        }

        public void Move(Vector2 location)
        {
            if (!IsMoving)
            {
                Location = location;
                IsMoving = true;
                if (GetAnimatedTexture() != null)
                {
                    GetAnimatedTexture().Row = (int)Direction;
                }
            }
        }

        public void MoveDown()
        {
            Direction = TeelEngine.Direction.South;
            Move(new Vector2(Location.X, Location.Y + 1));
            Velocity.Y = Speed;
        }
        public void MoveUp()
        {
            Direction = TeelEngine.Direction.North;
            Move(new Vector2(Location.X, Location.Y - 1));
            Velocity.Y = -Speed;
        }
        public void MoveLeft()
        {
            Direction = TeelEngine.Direction.West;
            Move(new Vector2(Location.X - 1, Location.Y));
            Velocity.X = -Speed;
        }
        public void MoveRight()
        {
            Direction = TeelEngine.Direction.East;
            Move(new Vector2(Location.X + 1, Location.Y));
            Velocity.X = Speed;
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
