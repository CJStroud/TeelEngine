using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeelEngine;
using TeelEngine.Level;
using TeelEngine.Pathing;
using TeelEngine.Render;

namespace TurnBasedGame
{
    public class GameStateDefault : GameState
    {
        private Level _level;
        private Renderer _renderer;
        private KeyController _keyController;
        private int ScreenWidth;
        private int ScreenHeight;

        public GameStateDefault(string name, int screenWidth, int screenHeight) : base(name)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public override void Initialize()
        {
            _level = new Level(50, 50);
            _keyController = new KeyController();

            _keyController.Add("MoveCameraUp", Keys.Up, () =>
            {
                var y = Camera.Lens.Y - 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            _keyController.Add("MoveCameraDown", Keys.Down, () =>
            {
                var y = Camera.Lens.Y + 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            _keyController.Add("MoveCameraLeft", Keys.Left, () =>
            {
                var x = Camera.Lens.X - 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            _keyController.Add("MoveCameraRight", Keys.Right, () =>
            {
                var x = Camera.Lens.X + 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            _keyController.Add("CameraZoomIn", Keys.Z, () =>
            {
                _renderer.GameTileSize += 1;
            });

            _keyController.Add("CameraZoomOut", Keys.X, () =>
            {
                _renderer.GameTileSize -= 1;
            });
        }

        public override void LoadContent(ContentManager contentManager)
        {
            CollisionDetection.Collisions = new List<Point>();

            for (int t = 0; t < 25; t++)
            {
                CollisionDetection.Collisions.Add(new Point(t, 10));
            }

            var spriteSheets = new List<SpriteSheet>();

            var bg = contentManager.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheets.Add(spriteSheetBg);

            var fg = contentManager.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheets.Add(spriteSheetFg);

            var woman = contentManager.Load<Texture2D>("Spritesheets/woman");
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheets.Add(spriteSheetWo);

            _renderer = new Renderer(spriteSheets, 32);

            var spriteTextureGrass = new SpriteTexture(spriteSheetBg.Name, 0);
            var spriteTextureBed = new SpriteTexture(spriteSheetFg.Name, 6);
            var animations = new Dictionary<string, int>();

            animations.Add("MOVE_UP", 0);
            animations.Add("MOVE_RIGHT", 1);
            animations.Add("MOVE_DOWN", 2);
            animations.Add("MOVE_LEFT", 3);

            var animatedTexture = new AnimatedTexture(spriteSheetWo.Name, new Point(0, 0), 8, true, spriteSheetWo.ColumnCount);

            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    var bTile = new TerrainTile { Texture = spriteTextureGrass, Rotation = 0F, Layer = 0 };
                    _level.AddTile(bTile, new Point(x, y) );
                }
            }

            //foreground test
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(3, 3));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(4, 3));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(5, 3));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(6, 3));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(7, 3));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 999 }, new Point(8, 3));

            //background test
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(3, 6));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(4, 6));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(5, 6));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(6, 6));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(7, 6));
            _level.AddTile(new TerrainTile { Texture = spriteTextureBed, Layer = 0 }, new Point(8, 6));

            var entity = new MoveableAnimatableEntity
            {
                Location = new Vector2(5, 5),
                Speed = 0.08F,
                Animations = animations,
                Texture = animatedTexture,
                NewLocation = new Vector2(4F, 4F),
                Layer = 1
            };
            _level.AddEntity(entity);

            _keyController.Add("PlayerMoveUp", Keys.W, () => entity.Move(Direction.North));
            _keyController.Add("PlayerMoveRight", Keys.D, () => entity.Move(Direction.East));
            _keyController.Add("PlayerMoveDown", Keys.S, () => entity.Move(Direction.South));
            _keyController.Add("PlayerMoveLeft", Keys.A, () => entity.Move(Direction.West));

        }

        public override void Update(GameTime gameTime)
        {
            _keyController.CheckKeyPresses(Keyboard.GetState());

            foreach (var gameTile in _level.GameTiles)
            {
                if (gameTile != null) gameTile.Update(_level, gameTime);
            }
            foreach (var entity in _level.Entities)
            {
                entity.Update(_level, gameTime);
            }

            var moveableEntity = _level.Entities[0] as MoveableEntity;

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mousePosition.X < ScreenWidth &&
                    mousePosition.Y < ScreenHeight)
                {
                    Console.WriteLine(mousePosition);

                    var start = new Point((int)moveableEntity.Location.X, (int)moveableEntity.Location.Y);
                    var end = Camera.GetGridCoordsWherePixelLocationIs(_renderer.GameTileSize, mousePosition);

                    PathFinder pathFinder = new PathFinder(100, 100, CollisionDetection.Collisions);
                    Path path = pathFinder.FindPath(start, end);

                    moveableEntity.Path = path;
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            _renderer.Render(_level.GetAllRenderables(), spriteBatch);
            
            spriteBatch.End();
        }
    }
}
