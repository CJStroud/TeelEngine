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
        private List<IGameState> gameStates; 

        public TurnBasedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

        }

        protected override void Initialize()
        {
            GameStateDefault gameStateDefault = new GameStateDefault(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            gameStates = new List<IGameState>();
            gameStates.Add(gameStateDefault);

            foreach (var gameState in gameStates)
            {
                gameState.Initialize();
            }

            IsMouseVisible = true;
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Camera.Lens = new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            foreach (var gameState in gameStates)
            {
                gameState.LoadContent(Content);
            }

        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var gameState in gameStates)
            {
                gameState.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (var gameState in gameStates)
            {
                gameState.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime);

        }
    }
}
