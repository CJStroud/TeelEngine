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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        GameRenderer gameRenderer; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            level = new Level(30, 30);

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

            gameRenderer = new GameRenderer(spriteSheets, 16);


            TerrainTile tTile = new TerrainTile {AssetName = fg.Name, TextureId = 25};
            level.AddTile(tTile, new Point(1, 1));

            Camera.Lens = new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            gameRenderer.Render(level, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
