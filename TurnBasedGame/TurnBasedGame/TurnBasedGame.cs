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
            List<Texture2D> spriteSheets = new List<Texture2D>();
            Texture2D bg = Content.Load<Texture2D>("Spritesheets/background");
            bg.Name = "background";
            spriteSheets.Add(bg);

            Texture2D fg = Content.Load<Texture2D>("Spritesheets/foreground");
            fg.Name = "foreground";
            spriteSheets.Add(fg);

            Texture2D woman = Content.Load<Texture2D>("Spritesheets/spritesheet");
            woman.Name = "woman";
            spriteSheets.Add(woman);

            _tileRenderer = new TileRenderer(spriteSheets, 16);

            var bTile = new TerrainTile {AssetName = bg.Name, TextureId = 10};
            var tTile = new TerrainTile {AssetName = fg.Name, TextureId = 6};

            var animations = new Dictionary<string, int>();

            animations.Add("MOVE_UP", 0);
            animations.Add("MOVE_RIGHT", 1);
            animations.Add("MOVE_DOWN", 2);
            animations.Add("MOVE_LEFT", 3);

            var eTile = new EntityTile {AssetName = woman.Name, Animations = animations};

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y ++)
                {
                    level.AddTile(bTile, new Point(x, y));
                }
            }

            level.AddTile(tTile, new Point(1, 1));
            level.AddTile(tTile, new Point(40, 40));
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
                    eTile.CurrentAnimation = "MOVE_UP";
                }
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
