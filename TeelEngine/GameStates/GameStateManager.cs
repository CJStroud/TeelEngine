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
        readonly Dictionary<string, GameState> _gameStates = new Dictionary<string, GameState>();
        public GameState CurrentGameState;

        public bool Add(GameState gameState)
        {
            if (_gameStates.ContainsKey(gameState.Name)) return false;

            _gameStates.Add(gameState.Name, gameState);

            if (_gameStates.Values.Count == 1) CurrentGameState = _gameStates[gameState.Name];            

            return true;
        }

        public bool SwapState(string state)
        {
            if (!_gameStates.ContainsKey(state) || CurrentGameState.Name == state) return false;

            CurrentGameState = _gameStates[state];
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
            foreach (var gameState in _gameStates.Values)
            {
                gameState.Initialize();
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var gameState in _gameStates.Values)
            {
                gameState.LoadContent(contentManager);
            }
        }

    }
}
