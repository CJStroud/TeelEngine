using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine 
{
    public class SpriteTexture : ITexture
    {
        public string AssetName { get; set; }
        public Texture2D Texture { get; set; }
        public bool ReadyToRender { get; set; }

        private int _width;

        public int SpriteSize { get; set; }

        public SpriteTexture()
        {
            
        }

        public void Render(SpriteBatch spriteBatch, int index, Vector2 screenPos)
        {
            if (!ReadyToRender && index >= 0) return;
            int x = index%_width;
            int y = index/_width;
            
            //Rectangle sourceRectangle = new Rectangle(x * SpriteSize, y * SpriteSize, SpriteSize, SpriteSize);
            //Rectangle destinationRectangle = new Rectangle((int) screenPos.X, (int) screenPos.Y, Globals.TileSize,
            //    Globals.TileSize);
            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);


        }

        public void LoadContent(ContentManager contentManager)
        {
            if (AssetName != null && SpriteSize != 0)
            {
                Texture = contentManager.Load<Texture2D>(AssetName);
                _width = Texture.Width/SpriteSize;
                ReadyToRender = true;
            }
        }

        public void LoadContent(ContentManager contentManager, string assetName, int spriteSize)
        {
            if (assetName != null)
            {
                AssetName = assetName;
                SpriteSize = spriteSize;
                LoadContent(contentManager);
            }
        }

        public void UnloadContent()
        {
            
        }
    }
}
