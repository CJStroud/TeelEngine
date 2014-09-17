using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace TeelEngine.Audio
{
    public class AudioManager : GameComponent
    {
        #region Constructor

        public AudioManager(Game game) : base(game)
        {
            
        }

        #endregion

        #region Public Properties

        public bool IsSongPlaying { get { return _isSongPlaying; } }
        public bool IsSoundEffectPlaying { get { return _isSoundEffectPlaying; } }
        public bool LoopSongs { get; set; }

        public float MusicVolume
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value; }
        }
        public float SoundEffectVolume
        {
            get { return SoundEffect.MasterVolume; }
            set { SoundEffect.MasterVolume = value; }
        }

        #endregion

        #region Private Fields

        private ContentManager _content;

        private Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        private Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();

        private Song _currentSong;

        private bool _isSongPlaying;
        private bool _isSongPaused;

        private SoundEffect _currentSoundEffect;
        private bool _isSoundEffectPlaying;
        private bool _isSoundEffectPaused;

        private SoundEffectInstance[] _soundEffectsBeingPlayed = new SoundEffectInstance[MaxSounds];
        private const int MaxSounds = 16;

        #endregion

        #region Overriden

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _soundEffectsBeingPlayed.Length; i++)
            {
                if (_soundEffectsBeingPlayed[i] != null && _soundEffectsBeingPlayed[i].State == SoundState.Stopped)
                {
                    _soundEffectsBeingPlayed[i].Dispose();
                    _soundEffectsBeingPlayed[i] = null;
                    _isSoundEffectPlaying = false;
                }
            }

            if (_currentSong != null && MediaPlayer.State == MediaState.Stopped)
            {
                _currentSong = null;
            }


            base.Update(gameTime);
        }

        #endregion

        #region Public Methods

        public void LoadContent(string contentFolder)
        {
            _content = contentFolder != String.Empty
                ? new ContentManager(Game.Content.ServiceProvider, Game.Content.RootDirectory + "\\" + contentFolder)
                : new ContentManager(Game.Content.ServiceProvider, Game.Content.RootDirectory);
        }

        public void LoadSoundEffect(string soundName, string soundPath)
        {
            if (_soundEffects.ContainsKey(soundName)) return;

            _soundEffects.Add(soundName, _content.Load<SoundEffect>(soundPath));
        }

        public void PlaySoundEffect(string soundName)
        {
            SoundEffect soundEffect;
            _soundEffects.TryGetValue(soundName, out soundEffect);

            if (soundEffect != null)
            {
                _soundEffectsBeingPlayed[0] = soundEffect.CreateInstance();
                _soundEffectsBeingPlayed[0].Play();
                _isSoundEffectPlaying = true;
            }

        }

        public void AddSong(Song song)
        {

        }

        public void RemoveSong()
        {
            
        }

        public void PlaySong()
        {

        }

        public void PauseSong()
        {
            
        }

        public void ResumeSong()
        {
            
        }

        public void StopSong()
        {
            
        }


        #endregion
    }
}
