using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeelEngine.GameStates;
using TeelEngine;
using TeelEngine.Audio;
using TeelEngine.Pathing;


namespace TurnBasedGame
{
    public class TurnBasedGame : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private GameStateManager _gameStateManager;

        private AudioManager _audioManager;

        public TurnBasedGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1600;

        }

        protected override void Initialize()
        {
            _gameStateManager = new GameStateManager();

            var gameStateDefault = new GameStateDefault("Default", _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            _gameStateManager.Add(gameStateDefault);

            _gameStateManager.Initialize();

            IsMouseVisible = true;

            // GameComponent Example
            _audioManager = new AudioManager(this);
            Components.Add(_audioManager);
            // End 

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            Camera.Lens = new Rectangle(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            _gameStateManager.LoadContent(Content);

            _audioManager.LoadContent();
            _audioManager.LoadSoundEffect("footstep", "Sounds/footsteps");
            _audioManager.LoadSong("test", "Songs/Dan Bull - John Lennon");

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            _gameStateManager.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _audioManager.PlaySong("test", true);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) && _audioManager.IsSongPlaying)
            {
                _audioManager.PauseSong();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) && _audioManager.IsSongPaused)
            {
                _audioManager.ResumeSong();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _audioManager.StopSong();
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _gameStateManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);

        }
    }
}
