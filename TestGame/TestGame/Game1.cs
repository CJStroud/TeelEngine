using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TeelEngine;

namespace TestGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        MenuController _menuController = new MenuController();
        LayerController layerController = new LayerController();
        private AnimatedTexture texture;
        private SpriteTexture spriteTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Camera.Lens = new Rectangle(0,0, 1000, 1000);
            texture = new AnimatedTexture(new Vector2(1,2), 3, 4, 6, true);
            spriteTexture = new SpriteTexture();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture.LoadContent(Content, "Textures/spritesheet");
            spriteTexture.LoadContent(Content, "Textures/chipset01", 16);
            Globals.TextureController.Add("Unit", texture);
            Globals.TextureController.Add("Background", spriteTexture);

            layerController = Content.Load<LayerController>("LayerController");
            layerController.Initialize();

            var settings = new XmlWriterSettings();
            settings.Indent = true;

            using (var writer = XmlWriter.Create("LayerController.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, layerController, null);
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                int x = Camera.Lens.X + 1;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
               int x = Camera.Lens.X - 1;
               Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                int y = Camera.Lens.Y - 1;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                int y = Camera.Lens.Y + 1;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }
            Globals.TextureController.Update(gameTime);
            layerController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            
            //_menuController.Render(_spriteBatch);

            layerController.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
