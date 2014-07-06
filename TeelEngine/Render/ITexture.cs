using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ITexture
    {
        string AssetName { get; set; }

        Texture2D Texture { get; set; }

        bool ReadyToRender { get; set; }

        void LoadContent(ContentManager contentManager);
        void UnloadContent();
    }
}
