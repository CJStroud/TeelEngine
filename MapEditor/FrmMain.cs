using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TeelEngine;
using TeelEngine.Loading;
using TeelEngine.Render;

namespace MapEditor
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            Loader.Load("level.xml");

            renderDisplay1.OnIntialize += (renderDisplay1_OnInitialize);
            renderDisplay1.OnDraw += RenderDisplay1OnDraw;

            var spriteSheets = new List<SpriteSheet>();

            var bg = contentManager.Load<Texture2D>("Spritesheets/background");
            var spriteSheetBg = new SpriteSheet("background", 16, bg);
            spriteSheets.Add(spriteSheetBg);

            var fg = contentManager.Load<Texture2D>("Spritesheets/foreground");
            var spriteSheetFg = new SpriteSheet("foreground", 16, fg);
            spriteSheets.Add(spriteSheetFg);

            Texture2D woman = Texture2D.FromStream(_graphics, "Spritesheets/woman"))
            var spriteSheetWo = new SpriteSheet("woman", 32, woman);
            spriteSheets.Add(spriteSheetWo);

            _renderer = new Renderer(spriteSheets, 32);

            InitializeComponent();

        }

        private Renderer _renderer;

        readonly GraphicsDeviceManager _graphics;


        private void renderDisplay1_OnInitialize(object sender, EventArgs e)
        {
            _renderer.Render(_level.GetAllRenderables(), spriteBatch);
        }

        private void RenderDisplay1OnDraw(object sender, EventArgs e)
        {
        }

    }
}
