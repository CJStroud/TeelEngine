using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.GameStates
{
    public class GameStateManager
    {
        Dictionary<string, GameState> gameStates = new Dictionary<string, GameState>();
        public GameState CurrentGameState;

        public bool Add(GameState gameState)
        {
            if (gameStates.ContainsKey(gameState.Name)) return false;

            gameStates.Add(gameState.Name, gameState);

            if (gameStates.Values.Count == 1) CurrentGameState = gameStates[gameState.Name];            

            return true;
        }

        public bool SwapState(string state)
        {
            if (!gameStates.ContainsKey(state)) return false;

            CurrentGameState = gameStates[state];
            return true;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentGameState != null)
            {
                CurrentGameState.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (CurrentGameState != null)
            {
                CurrentGameState.Draw(gameTime, spriteBatch);
            }
        }

        public void Initialize()
        {
            foreach (var gameState in gameStates.Values)
            {
                gameState.Initialize();
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var gameState in gameStates.Values)
            {
                gameState.LoadContent(contentManager);
            }
        }

    }
}
