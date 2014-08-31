using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public class AnimatedTexture : SpriteTexture
    {
        private Point _frame;

        public int Column
        {
            get { return (int)_frame.X; }
            set
            {
                _frame = new Point(value, _frame.Y);
            }
        }
        public int Row
        {
            get { return (int)_frame.Y; }
            set
            {
                _frame = new Point(_frame.X, value);
            }
        }

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

        private int ColumnCount;

        public AnimatedTexture(string assetName, Point frame, int framePerSec, bool paused, int columnCount) : base(assetName, 0)
        {
            _frame = frame;
            _paused = paused;
            ColumnCount = columnCount;
            Paused = false;
            TotalElapsed = 0;
            TimePerFrame = (float) 1/framePerSec;
        }

        public override void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet)
        {
            Render(spriteBatch, screenPos, gameTileSize, spriteSheet, 0F);
        }

        public override void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet, float rotation)
        {
            if (ColumnCount == 0) ColumnCount = spriteSheet.ColumnCount;

            var souceRectangle = spriteSheet.GetTileRectangle(_frame);

            var destinationRectangle = new Rectangle(screenPos.X, screenPos.Y, gameTileSize, gameTileSize);

            spriteBatch.Draw(spriteSheet.Texture, destinationRectangle, souceRectangle, Color.White, rotation, new Vector2(spriteSheet.TileSize / 2F, spriteSheet.TileSize / 2F), SpriteEffects.None, 0);
        }

        public void NextFrame(float elapsed)
        {
            if (Paused)
            {
                _frame = new Point(TextureId, Row);
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
    }
}
