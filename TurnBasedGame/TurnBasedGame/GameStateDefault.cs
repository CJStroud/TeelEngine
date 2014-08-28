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
        private Level level;
        private Renderer _renderer;
        private TileRenderer _tileRenderer;
        private EntityRenderer _entityRenderer;
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
            level = new Level(50, 50);
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
                _entityRenderer.GameTileSize += 1;
                _tileRenderer.GameTileSize += 1;
            });

            _keyController.Add("CameraZoomOut", Keys.X, () =>
            {
                _entityRenderer.GameTileSize -= 1;
                _tileRenderer.GameTileSize -= 1;
            });
        }

        public override void LoadContent(ContentManager contentManager)
        {
            CollisionDetection.Collisions = new List<Point>();

            for (int t = 0; t < 25; t++)
            {
                CollisionDetection.Collisions.Add(new Point(t, 10));
            }

            var spriteSheetsTerrain = new List<SpriteSheet>();
            var spriteSheetsEntities = new List<SpriteSheet>();
            var spriteSheets = new List<SpriteSheet>();

            var bg = contentManager.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheetsTerrain.Add(spriteSheetBg);
            spriteSheets.Add(spriteSheetBg);

            var fg = contentManager.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheetsTerrain.Add(spriteSheetFg);
            spriteSheets.Add(spriteSheetFg);

            var woman = contentManager.Load<Texture2D>("Spritesheets/woman");
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheetsEntities.Add(spriteSheetWo);
            spriteSheets.Add(spriteSheetWo);

            _tileRenderer = new TileRenderer(spriteSheetsTerrain, 32);
            _entityRenderer = new EntityRenderer(spriteSheetsEntities, 32);
            _renderer = new Renderer(spriteSheets, 32);

            var spriteTextureGrass = new SpriteTexture(spriteSheetBg.Name, 0);
            var spriteTextureBed = new SpriteTexture(spriteSheetFg.Name, 6);

            var bTile = new TerrainTile { Texture = spriteTextureGrass, Rotation = 0F, Layer = 0};
            var tTile = new TerrainTile { Texture = spriteTextureBed, Layer = 999};

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
                    level.AddTile(bTile, new Point(x, y) );
                }
            }

            level.AddTile(tTile, new Point(1, 1));
            level.AddTile(tTile, new Point(40, 40));


            var entity = new MoveableAnimatableEntity
            {
                Location = new Vector2(5, 5),
                Speed = 0.08F,
                Animations = animations,
                Texture = animatedTexture,
                NewLocation = new Vector2(4F, 4F),
                Layer = 1
            };
            level.AddEntity(entity);

            _keyController.Add("PlayerMoveUp", Keys.W, () => entity.Move(Direction.North));
            _keyController.Add("PlayerMoveRight", Keys.D, () => entity.Move(Direction.East));
            _keyController.Add("PlayerMoveDown", Keys.S, () => entity.Move(Direction.South));
            _keyController.Add("PlayerMoveLeft", Keys.A, () => entity.Move(Direction.West));

        }

        public override void Update(GameTime gameTime)
        {
            _keyController.CheckKeyPresses(Keyboard.GetState());

            foreach (var gameTile in level.GameTiles)
            {
                if (gameTile != null) gameTile.Update(level, gameTime);
            }
            foreach (var entity in level.Entities)
            {
                entity.Update(level, gameTime);
            }

            var moveableEntity = level.Entities[0] as MoveableEntity;

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mousePosition.X < ScreenWidth &&
                    mousePosition.Y < ScreenHeight)
                {
                    Console.WriteLine(mousePosition);

                    var start = new Point((int)moveableEntity.Location.X, (int)moveableEntity.Location.Y);
                    var end = Camera.GetGridCoordsWherePixelLocationIs(_tileRenderer.GameTileSize, mousePosition);

                    PathFinder pathFinder = new PathFinder(100, 100, CollisionDetection.Collisions);
                    Path path = pathFinder.FindPath(start, end);

                    moveableEntity.Path = path;
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
/*
            _tileRenderer.Render(level, spriteBatch);
            _entityRenderer.Render(level, spriteBatch);
*/

            _renderer.Render(level, spriteBatch);
            
            spriteBatch.End();
        }
    }
}
