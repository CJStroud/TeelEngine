﻿using System;
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
        public int TextureId { get; set; }
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

        public AnimatedTexture(string assetName, Point frame, int framePerSec, bool paused, int columnCount)
        {
            AssetName = assetName;
            _frame = frame;
            _paused = paused;
            ColumnCount = columnCount;
            Paused = false;
            TotalElapsed = 0;
            TimePerFrame = (float) 1/framePerSec;
        }

        public void Render(SpriteBatch spriteBatch, Point screenPos, int gameTileSize, SpriteSheet spriteSheet)
        {
            if (ColumnCount == 0) ColumnCount = spriteSheet.ColumnCount;

            var souceRectangle = spriteSheet.GetTileRectangle(_frame);

            var destinationRectangle = new Rectangle(screenPos.X, screenPos.Y, gameTileSize, gameTileSize);

            spriteBatch.Draw(spriteSheet.Texture, destinationRectangle, souceRectangle, Color.White);
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
