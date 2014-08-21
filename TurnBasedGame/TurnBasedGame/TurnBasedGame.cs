using System;
using System.Collections.Generic;
using System.Linq;
using aStarPathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using TeelEngine;
using TeelEngine.Level;

namespace TurnBasedGame
{
    public class TurnBasedGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        TileRenderer _tileRenderer;
        private EntityRenderer _entityRenderer;

        public TurnBasedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            level = new Level(50, 50);

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {



            spriteBatch = new SpriteBatch(GraphicsDevice);
            var spriteSheetsTerrain = new List<SpriteSheet>();
            var spriteSheetsEntities = new List<SpriteSheet>();

            var bg = Content.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheetsTerrain.Add(spriteSheetBg);

            var fg = Content.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheetsTerrain.Add(spriteSheetFg);

            var woman = Content.Load<Texture2D>("Spritesheets/woman");
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheetsEntities.Add(spriteSheetWo);

            _tileRenderer = new TileRenderer(spriteSheetsTerrain, 32);
            _entityRenderer = new EntityRenderer(spriteSheetsEntities, 32);

            var spriteTextureGrass = new SpriteTexture(spriteSheetBg.Name, 145);
            var spriteTextureBed = new SpriteTexture(spriteSheetFg.Name, 6);

            var bTile = new TerrainTile {Texture = spriteTextureGrass, Rotation = 0F};
            var tTile = new TerrainTile {Texture = spriteTextureBed};

            var animations = new Dictionary<string, int>();

            animations.Add("MOVE_UP", 0);
            animations.Add("MOVE_RIGHT", 1);
            animations.Add("MOVE_DOWN", 2);
            animations.Add("MOVE_LEFT", 3);

            var animatedTexture = new AnimatedTexture(spriteSheetWo.Name, new Point(0, 0), 8, true,  spriteSheetWo.ColumnCount);

            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 50; y ++)
                {
                    level.AddTile(bTile, new Point(x, y));
                }
            }

            level.AddTile(tTile, new Point(1, 1));
            level.AddTile(tTile, new Point(40, 40));


            var entity = new MoveableAnimatableEntity {Location = new Vector2(5,5), Speed = 0.08F, Animations = animations, Texture = animatedTexture, NewLocation = new Vector2(4F, 4F)};
            level.AddEntity(entity);

            Camera.Lens = new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                var x = Camera.Lens.X + 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                var x = Camera.Lens.X - 10;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                var y = Camera.Lens.Y - 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                var y = Camera.Lens.Y + 10;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }

            var moveableEntity = (MoveableEntity)level.Entities[0];

            if (keyboardState.IsKeyDown(Keys.W))
            {
                moveableEntity.Move(Direction.North);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                moveableEntity.Move(Direction.East);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                moveableEntity.Move(Direction.South);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                moveableEntity.Move(Direction.West);
            }



            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mousePosition.X < GraphicsDevice.PresentationParameters.BackBufferWidth &&
                    mousePosition.Y < GraphicsDevice.PresentationParameters.BackBufferHeight)
                {
                    Console.WriteLine(mousePosition);

                    var start = new Point((int)moveableEntity.Location.X, (int)moveableEntity.Location.Y);
                    var end = Camera.GetGridCoordsWherePixelLocationIs(_tileRenderer.GameTileSize, mousePosition);

                    PathFinder pathFinder = new PathFinder(100, 100, CollisionDetection.Collisions);
                    pathFinder.Create(start, end);

                    Path path = pathFinder.Path;

                    moveableEntity.Path = path;
                }
            }


            foreach (var gameTile in level.GameTiles)
            {
                if(gameTile != null)gameTile.Update(level, gameTime);
            }
            foreach (var entity in level.Entities)
            {
                entity.Update(level, gameTime);
            }

            if (keyboardState.IsKeyDown(Keys.X))
            {
                _entityRenderer.GameTileSize += 2;
                _tileRenderer.GameTileSize += 2;
            }
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                _entityRenderer.GameTileSize -= 2;
                _tileRenderer.GameTileSize -= 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            _tileRenderer.Render(level, spriteBatch);
            _entityRenderer.Render(level, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
