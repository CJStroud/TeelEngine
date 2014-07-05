using System;
using System.Collections.Generic;
using System.Linq;
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
        public Camera Camera;


        private Texture2D unitTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            Camera = new Camera(new Point(0,0), Globals.TileSize, Globals.TileSize);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            unitTexture = Content.Load<Texture2D>("Unit");
            unit = new Unit(unitTexture);
            unit.Location = new Vector2(0.5F, 0.5F);
            var entityLayer = new EntityLayer();
            entityLayer.AddEntity(unit);

            layerController.Add(entityLayer);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            layerController.Render(_spriteBatch, Camera);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
