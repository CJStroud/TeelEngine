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

        public TurnBasedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            level = new Level(50, 50);

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var spriteSheets = new List<SpriteSheet>();
            var bg = Content.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheets.Add(spriteSheetBg);

            var fg = Content.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheets.Add(spriteSheetFg);

            var woman = Content.Load<Texture2D>("Spritesheets/woman");
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheets.Add(spriteSheetWo);

            _tileRenderer = new TileRenderer(spriteSheets, 16);

            var spriteTextureGrass = new SpriteTexture(spriteSheetBg.Name, 29);
            var spriteTextureBed = new SpriteTexture(spriteSheetFg.Name, 6);

            var bTile = new TerrainTile {Texture = spriteTextureGrass};
            var tTile = new TerrainTile {Texture = spriteTextureBed};

            var animations = new Dictionary<string, int>();

            animations.Add("MOVE_UP", 0);
            animations.Add("MOVE_RIGHT", 1);
            animations.Add("MOVE_DOWN", 2);
            animations.Add("MOVE_LEFT", 3);

            var animatedTexture = new AnimatedTexture(spriteSheetWo.Name, new Point(0, 0), 3, true,  spriteSheetWo.ColumnCount);

            var eTile = new EntityTile { Animations = animations, Texture = animatedTexture};

            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 50; y ++)
                {
                    level.AddTile(bTile, new Point(x, y));
                }
            }

            level.AddTile(tTile, new Point(1, 1));
            level.AddTile(tTile, new Point(40, 40));


            var entity = new MoveableEntity() {Location = new Point(5,5), Speed = 0.2F};
            level.Entities.Add(entity);

            eTile.EntityId = 0;

            level.AddTile(eTile, new Point(5, 5));

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
            if (keyboardState.IsKeyDown(Keys.W))
            {
                var eTile = level.GameTiles[5, 5].SubTiles.Last() as EntityTile;
                if (eTile != null)
                {
                    eTile.Move(0, -1);
                }
            }

            foreach (var gameTile in level.GameTiles)
            {
                if(gameTile != null)gameTile.Update(level, gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            _tileRenderer.Render(level, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
