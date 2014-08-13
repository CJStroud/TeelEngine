using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class AnimatedTexture : ITexture
    {
        public string AssetName { get; set; }
        public Texture2D Texture { get; set; }
        public bool ReadyToRender { get; set; }
        private Vector2 _frame;

        public int Column
        {
            get { return (int)_frame.X; }
            set
            {
                _frame = new Vector2(value, _frame.Y);
            }
        }
        public int Row
        {
            get { return (int)_frame.Y; }
            set
            {
                _frame = new Vector2(_frame.X, value);
            }
        }

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }

        private bool _paused;
        public bool Paused
        {
            get { return _paused; }
            set
            {
                _paused = value;
                if (_paused)
                {
                    Column = 0;
                }
            }
        }

        public float TotalElapsed { get; set; }
        public float TimePerFrame { get; set; }

        public AnimatedTexture(Vector2 frame, int columnCount, int rowCount, int framePerSec, bool paused)
        {
            _frame = frame;
            ColumnCount = columnCount;
            RowCount = rowCount;
            _paused = paused;
            Paused = false;
            TotalElapsed = 0;
            TimePerFrame = (float) 1/framePerSec;
        }

        //public void Render(SpriteBatch spriteBatch, Vector2 screenPos)
        //{
        //    Render(spriteBatch, screenPos, _frame);
        //}
        //public void Render(SpriteBatch spriteBatch, Vector2 screenPos, Vector2 frame)
        //{
        //    if (!ReadyToRender) return;
        //    Rectangle souceRectangle = new Rectangle((int) (FrameWidth*frame.X), (int) (FrameHeight*frame.Y), FrameWidth, FrameHeight);

        //    float width = Globals.TileSize;
        //    float height = Globals.TileSize;
        //    int offsetX = 0;
        //    int offsetY = 0;

        //    if (FrameWidth > FrameHeight)
        //    {
        //        width = Globals.TileSize;
        //        height = ((float)Globals.TileSize)/((float)FrameWidth/(float)FrameHeight);

        //        offsetY = (int) ((Globals.TileSize - height)/2);
        //    }
        //    else if(FrameHeight > FrameWidth)
        //    {
        //        width = Globals.TileSize/((float) FrameHeight/(float) FrameWidth);
        //        height = Globals.TileSize;

        //        offsetX = (int) ((Globals.TileSize - width)/2);
        //    }




        //    Rectangle destinationRectangle = new Rectangle((int) screenPos.X + offsetX, (int) screenPos.Y + offsetY, (int)width, (int)height);
        //    spriteBatch.Draw(Texture, destinationRectangle, souceRectangle, Color.White);
        //}

        public void NextFrame(float elapsed)
        {
            if (Paused)
            {
                return;
            }
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                _frame.X++;
                _frame.X = _frame.X%ColumnCount;
                TotalElapsed -= TimePerFrame;
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            if (AssetName != null)
            {
                Texture = contentManager.Load<Texture2D>(AssetName);
                ReadyToRender = true;
                FrameWidth = Texture.Width/ColumnCount;
                FrameHeight = Texture.Height/RowCount;
            }
        }

        public void LoadContent(ContentManager contentManager, string assetName)
        {
            if (assetName != null)
            {
                AssetName = assetName;
                LoadContent(contentManager);
            }
        }

        public void UnloadContent()
        {
            Texture.Dispose();
        }
    }
}
