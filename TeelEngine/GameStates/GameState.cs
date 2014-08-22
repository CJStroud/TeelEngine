using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public abstract class GameState
    {
        public string Name { get; set; }

        public virtual void Initialize(){}

        public virtual void LoadContent(ContentManager contentManager){}

        public virtual void Update(GameTime gameTime){}

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch){}

        protected GameState(string name)
        {
            Name = name;
        }
    }
}
