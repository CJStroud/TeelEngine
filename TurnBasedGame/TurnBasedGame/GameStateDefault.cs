using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeelEngine;
using TeelEngine.GUI;
using TeelEngine.Level;
using TeelEngine.Path;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TurnBasedGame
{
    public class GameStateDefault : GameState
    {
        private Level level;
        private TileRenderer _tileRenderer;
        private EntityRenderer _entityRenderer;
        private InputManager _inputManager;
        private int ScreenWidth;
        private int ScreenHeight;
        private Gui _testGui;
        private GuiContainer _testGuiContainer;
        private GuiContainer _testGuiScreen;

        public GameStateDefault(string name, int screenWidth, int screenHeight) : base(name)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public override void Initialize()
        {
            level = new Level(50, 50);
            _inputManager = new InputManager();

            _inputManager.AddAction("MoveCameraUp", () =>
            {
                var y = Camera.Lens.Y - 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            _inputManager.AddKeyBinding(Keys.Up, "MoveCameraUp");
            _inputManager.AddGamepadBinding(Buttons.LeftThumbstickUp, "MoveCameraUp");

            _inputManager.AddAction("MoveCameraDown", () =>
            {
                var y = Camera.Lens.Y + 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            });

            _inputManager.AddKeyBinding(Keys.Down, "MoveCameraDown");

            _inputManager.AddAction("MoveCameraLeft", () =>
            {
                var x = Camera.Lens.X - 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            _inputManager.AddKeyBinding(Keys.Left, "MoveCameraLeft");

            _inputManager.AddAction("MoveCameraRight", () =>
            {
                var x = Camera.Lens.X + 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            });

            _inputManager.AddKeyBinding(Keys.Right, "MoveCameraRight");

            _inputManager.AddAction("CameraZoomIn", () =>
            {
                _entityRenderer.GameTileSize += 1;
                _tileRenderer.GameTileSize += 1;
            });

            _inputManager.AddKeyBinding(Keys.Z, "CameraZoomIn");

            _inputManager.AddAction("CameraZoomOut", () =>
            {
                _entityRenderer.GameTileSize -= 1;
                _tileRenderer.GameTileSize -= 1;
            });

            _inputManager.AddKeyBinding(Keys.X, "CameraZoomOut");

            _testGuiScreen = new GuiContainer(new Point(0,0), ScreenWidth, ScreenHeight);

            _testGuiContainer = new GuiContainer(new Vector2(0F,0F), 1F, 0.1F) { BackColour = Color.Teal, Anchor = Anchor.TopMiddle, Opacity = 0.5F};

            _testGui = new GuiContainer(new Vector2(0F, 0F), 0.5F, 1.5F) { BackColour = Color.Red, Anchor = Anchor.TopMiddle, Opacity = 0.5F};

            _testGuiContainer.AddGui(_testGui);

            _testGuiScreen.AddGui(_testGuiContainer);

        }

        public override void LoadContent(ContentManager contentManager)
        {
            var spriteSheetsTerrain = new List<SpriteSheet>();
            var spriteSheetsEntities = new List<SpriteSheet>();

            var bg = contentManager.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheetsTerrain.Add(spriteSheetBg);

            var fg = contentManager.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheetsTerrain.Add(spriteSheetFg);

            var woman = contentManager.Load<Texture2D>("Spritesheets/woman");
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheetsEntities.Add(spriteSheetWo);

            _tileRenderer = new TileRenderer(spriteSheetsTerrain, 32);
            _entityRenderer = new EntityRenderer(spriteSheetsEntities, 32);

            var spriteTextureGrass = new SpriteTexture(spriteSheetBg.Name, 0);
            var spriteTextureBed = new SpriteTexture(spriteSheetFg.Name, 6);

            var bTile = new TerrainTile { Texture = spriteTextureGrass, Rotation = 0F };
            var tTile = new TerrainTile { Texture = spriteTextureBed };

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
                    level.AddTile(bTile, new Point(x, y));
                }
            }

            level.AddTile(tTile, new Point(1, 1));
            level.AddTile(tTile, new Point(40, 40));


            var entity = new MoveableAnimatableEntity { Location = new Vector2(5, 5), Speed = 0.08F, Animations = animations, Texture = animatedTexture, NewLocation = new Vector2(4F, 4F) };
            level.AddEntity(entity);

            _inputManager.AddAction("PlayerMoveUp", () => entity.Move(Direction.North));
            _inputManager.AddAction("PlayerMoveRight", () => entity.Move(Direction.East));
            _inputManager.AddAction("PlayerMoveDown", () => entity.Move(Direction.South));
            _inputManager.AddAction("PlayerMoveLeft", () => entity.Move(Direction.West));

            _inputManager.AddKeyBinding(Keys.W,  "PlayerMoveUp");
            _inputManager.AddKeyBinding(Keys.D, "PlayerMoveRight");
            _inputManager.AddKeyBinding(Keys.S, "PlayerMoveDown");
            _inputManager.AddKeyBinding(Keys.A, "PlayerMoveLeft");

        }

        public override void Update(GameTime gameTime)
        {
            

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
                    var end = new Point(mousePosition.X / _tileRenderer.GameTileSize, mousePosition.Y / _tileRenderer.GameTileSize);

                    PathFinder pathFinder = new PathFinder(100, 100, CollisionDetection.Collisions);
                    //pathFinder.Create(start, end);

                    //Path path = pathFinder.Path;

                    //moveableEntity.Path = path;
                }
            }

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F))
            {
                _testGuiScreen.SetWidth(_testGuiScreen.Width - 10);
            }

            if (state.IsKeyDown(Keys.G))
            {
                _testGuiScreen.SetWidth(_testGuiScreen.Width + 10);
            }

            if (state.IsKeyDown(Keys.V))
            {
                _testGuiScreen.SetHeight(_testGuiScreen.Height - 10);
            }

            if (state.IsKeyDown(Keys.B))
            {
                _testGuiScreen.SetHeight(_testGuiScreen.Height + 10);
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            _tileRenderer.Render(level, spriteBatch);
            _entityRenderer.Render(level, spriteBatch);
            _testGuiScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
