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
        public static LayerController layerController = new LayerController();
        public static KeyController keyController = new KeyController();
        private Path path;
        private Unit unit;

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
            this.IsMouseVisible = true;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture.LoadContent(Content, "Textures/spritesheet");
            spriteTexture.LoadContent(Content, "Textures/chipset01", 16);
            Globals.TextureController.Add("Unit", texture);
            Globals.TextureController.Add("Background", spriteTexture);
            CollisionDetection.Collisions = Content.Load<List<Vector2>>("Collisions");
            layerController = Content.Load<LayerController>("LayerController");
            layerController.Initialize();
            EntityLayer entityLayer = layerController.GetEntityLayer();
            unit = (Unit)entityLayer.Entities[0];
            keyController = Content.Load<KeyController>("Keycontroller");
            
            keyController.AddAction("Move Down", Events.PlayerMoveDown);
            keyController.AddAction("Move Up", Events.PlayerMoveUp);
            keyController.AddAction("Move Left", Events.PlayerMoveLeft);
            keyController.AddAction("Move Right", Events.PlayerMoveRight);

            //path = Path.CreatePath(new Point(0, 0), new Point(2, 2));
            //unit.Move(path);

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            
            using (var writer = XmlWriter.Create("Keycontroller.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, keyController, null);
            }
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mousePosition.X < GraphicsDevice.PresentationParameters.BackBufferWidth &&
                    mousePosition.Y < GraphicsDevice.PresentationParameters.BackBufferHeight)
                {
                    Console.WriteLine(mousePosition);

                    var start = new Point((int) unit.Location.X, (int) unit.Location.Y);
                    var end = new Point(mousePosition.X/Globals.TileSize, mousePosition.Y/Globals.TileSize);
                    path = Path.CreatePath(start, end);
                    unit.Move(path);
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                var x = Camera.Lens.X + 1;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
               var x = Camera.Lens.X - 1;
               Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                var y = Camera.Lens.Y - 1;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                var y = Camera.Lens.Y + 1;
                Camera.UpdateLensPosition(new Point(Camera.Lens.X, y));
            }

            Globals.TextureController.Update(gameTime);
            layerController.Update(gameTime);
            keyController.CheckKeyPresses(keyboardState);
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

