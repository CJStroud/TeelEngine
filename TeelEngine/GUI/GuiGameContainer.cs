using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine.Gui
{
    public class GuiGameContainer : GuiContainer
    {
        #region Constructors

        public GuiGameContainer(Texture2D texture, Point location, int width, int height, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(texture, location, width, height)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(Point location, int width, int height, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(location, width, height)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(List<BaseGui> children, Point location, int width, int height, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(children, location, width, height)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(Texture2D texture, Vector2 location, float width, float height, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(texture, location, width, height)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(Vector2 location, float width, float height, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(location, width, height)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(texture, location, width, height, maxWidth, maxHeight)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        public GuiGameContainer(Vector2 location, float width, float height, int maxWidth, int maxHeight, TileRenderer tileRenderer, EntityRenderer entityRenderer, Level.Level level) : base(location, width, height, maxWidth, maxHeight)
        {
            _tileRenderer = tileRenderer;
            _entityRenderer = entityRenderer;
            _level = level;
        }

        #endregion

        #region Private Globals

        private TileRenderer _tileRenderer;
        private EntityRenderer _entityRenderer;

        private Level.Level _level;

        #endregion

        #region Overriden Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            Camera.Lens = new Rectangle(Camera.Lens.X, Camera.Lens.Y, BoundingRectangle.Width, BoundingRectangle.Height);

            if (Visible){
            
                _tileRenderer.Render(_level, spriteBatch);
                _entityRenderer.Render(_level, spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public override void Update()
        {

            base.Update();
        }

        #endregion
    }
}
