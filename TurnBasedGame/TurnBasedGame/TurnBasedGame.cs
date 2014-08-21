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
using TeelEngine.GameStates;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using TeelEngine;
using TeelEngine.Level;

namespace TurnBasedGame
{
    public class TurnBasedGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameStateManager gameStateManager;

        public TurnBasedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

        }

        protected override void Initialize()
        {
            gameStateManager = new GameStateManager();

            GameStateDefault gameStateDefault = new GameStateDefault("Default", graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            gameStateManager.Add(gameStateDefault);

            gameStateManager.Initialize();

            IsMouseVisible = true;
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Camera.Lens = new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            gameStateManager.LoadContent(Content);

        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            gameStateManager.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);

        }
    }
}
