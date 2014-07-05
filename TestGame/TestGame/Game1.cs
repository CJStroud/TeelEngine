using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        private Unit unit;



        private Texture2D unitTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //layerController = MapLoader.Load(@"/Levels/Level1.xml");
        }

        protected override void Initialize()
        {
            Camera.Lens = new Rectangle(0,0, 100, 100);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            unitTexture = Content.Load<Texture2D>("Unit");
            unit = new Unit(unitTexture) {Location = new Vector2(0F, 0F)};
            
            var entityLayer = new EntityLayer();

            entityLayer.AddEntity(unit);
            unit = new Unit(unitTexture) { Location = new Vector2(-1F, 0F) };
            entityLayer.AddEntity(unit);

            Globals.texture = new Texture2D(GraphicsDevice, 1, 1);
            Globals.texture.SetData(new Color[] {Color.White});
            layerController.Add(entityLayer);

            IEnumerable<TerrainLayer> layer = layerController.GetTerrainLayers();
            layer.First().SpriteSheet = Content.Load<Texture2D>("TerrainSpriteSheet");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                int x = Camera.Lens.X + 1;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                int x = Camera.Lens.X - 1;
                Camera.UpdateLensPosition(new Point(x, Camera.Lens.Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            layerController.Render(_spriteBatch);
            //_menuController.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
