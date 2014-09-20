using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeelEngine;
using TeelEngine.Gui;
using TeelEngine.Input;
using TeelEngine.Level;
using TeelEngine.Pathing;
using TeelEngine.Render;
using TeelEngine.Loading;
using Keys = Microsoft.Xna.Framework.Input.Keys;


namespace TurnBasedGame
{
    public class GameStateDefault : GameState
    {
        private Level _level;
        private Renderer _renderer;
        private int ScreenWidth;
        private int ScreenHeight;
        private BaseGui _testBaseGui;
        private GuiContainer _testBaseGuiContainer;
        private GuiScreen _testBaseGuiScreen;
        private GuiGameContainer _testGuiGameContainer;

        public GameStateDefault(string name, int screenWidth, int screenHeight) : base(name)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public override void Initialize()
        {
            _level = new Level(50, 50);
            
            KeyManager.AddAction("MoveCameraUp", () =>
            {
                var y = Camera.Lens.Y - 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            KeyManager.AddKeyBinding(Keys.Up, "MoveCameraUp");
            KeyManager.AddGamepadBinding(Buttons.LeftThumbstickUp, "MoveCameraUp");

            KeyManager.AddAction("MoveCameraDown", () =>
            {
                var y = Camera.Lens.Y + 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            KeyManager.AddKeyBinding(Keys.Down, "MoveCameraDown");

            KeyManager.AddAction("MoveCameraLeft", () =>
            {
                var x = Camera.Lens.X - 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            KeyManager.AddKeyBinding(Keys.Left, "MoveCameraLeft");

            KeyManager.AddAction("MoveCameraRight", () =>
            {
                var x = Camera.Lens.X + 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            KeyManager.AddKeyBinding(Keys.Right, "MoveCameraRight");

            KeyManager.AddAction("CameraZoomIn", () =>
            {
                _renderer.GameTileSize += 1;
            });

            KeyManager.AddKeyBinding(Keys.Z, "CameraZoomIn");

            KeyManager.AddAction("CameraZoomOut", () =>
            {
                _renderer.GameTileSize -= 1;
            });

            KeyManager.AddKeyBinding(Keys.X, "CameraZoomOut");

            _testBaseGuiScreen = new GuiScreen(new Point(0,0), ScreenWidth, ScreenHeight);

            _testBaseGuiContainer = new GuiContainer(new Vector2(0F,0F), 1F, 0.1F) { BackColour = Color.Teal, Anchor = Anchor.TopMiddle, Opacity = 0.5F};

            _testBaseGui = new GuiContainer(new Vector2(0F, 0F), 0.5F, 1.5F) { BackColour = Color.Red, Anchor = Anchor.TopMiddle, Opacity = 0.5F};

        }

        public override void LoadContent(ContentManager contentManager)
        {
            CollisionDetection.Collisions = new List<Point>();

            for (int t = 0; t < 25; t++)
            {
                CollisionDetection.Collisions.Add(new Point(t, 10));
                if (t != 4)
                {
                    CollisionDetection.Collisions.Add(new Point(t, 12));
                }
                if (t != 15)
                {
                    CollisionDetection.Collisions.Add(new Point(t, 14));
                }

                if (t != 12)
                {
                    CollisionDetection.Collisions.Add(new Point(t, 16));
                }

                CollisionDetection.Collisions.Add(new Point(t, 18));
            }
            CollisionDetection.Collisions.Add(new Point(3, 11));
            CollisionDetection.Collisions.Add(new Point(20, 13));
            CollisionDetection.Collisions.Add(new Point(25, 15));
            CollisionDetection.Collisions.Add(new Point(25, 17));

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

            KeyManager.AddAction("Player.MoveUp", () => entity.Move(Direction.North));
            KeyManager.AddAction("Player.MoveRight", () => entity.Move(Direction.East));
            KeyManager.AddAction("Player.MoveDown", () => entity.Move(Direction.South));
            KeyManager.AddAction("Player.MoveLeft", () => entity.Move(Direction.West));

            KeyManager.AddKeyBinding(Keys.W,  "Player.MoveUp");
            KeyManager.AddKeyBinding(Keys.D, "Player.MoveRight");
            KeyManager.AddKeyBinding(Keys.S, "Player.MoveDown");
            KeyManager.AddKeyBinding(Keys.A, "Player.MoveLeft");

            _testGuiGameContainer = new GuiGameContainer(new Vector2(0, 0), 1, 0.8F);

            _testGuiGameContainer.OnPressHandler += (sender, args) =>
            {
                MouseState mouseState = MouseHandler.CurrentMouseState;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    var mousePosition = new Point(mouseState.X, mouseState.Y);
                    if (mousePosition.X < ScreenWidth &&
                        mousePosition.Y < ScreenHeight)
                    {
                        Console.WriteLine(mousePosition);

                        var start = new Point((int)entity.Location.X, (int)entity.Location.Y);
                        var end = Camera.GetGridCoordsWherePixelLocationIs(_renderer.GameTileSize, mousePosition);

                        PathFinder pathFinder = new PathFinder(100, 100, CollisionDetection.Collisions);
                        Path path = pathFinder.FindPath(start, end);

                        entity.Path = path;
                    }
                }

            };

            _testGuiGameContainer.OnDrawHandler += (sender, args) =>
            {
                _renderer.Render(_level.GetAllRenderables(), args.SpriteBatch);
            };

            _testBaseGuiScreen.AddGui(_testGuiGameContainer);

            _testBaseGuiScreen.AddGui(_testBaseGuiContainer);

            _testBaseGuiContainer.AddGui(_testBaseGui);
        }

        public override void Update(GameTime gameTime)
        {
            

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

            

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F))
            {
                _testBaseGuiScreen.SetWidth(_testBaseGuiScreen.Width - 10);
            }

            if (state.IsKeyDown(Keys.G))
            {
                _testBaseGuiScreen.SetWidth(_testBaseGuiScreen.Width + 10);
            }

            if (state.IsKeyDown(Keys.V))
            {
                _testBaseGuiScreen.SetHeight(_testBaseGuiScreen.Height - 10);
            }

            if (state.IsKeyDown(Keys.B))
            {
                _testBaseGuiScreen.SetHeight(_testBaseGuiScreen.Height + 10);
            }

            _testBaseGuiScreen.Update();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            _testBaseGuiScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
