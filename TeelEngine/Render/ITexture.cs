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
        int TextureId { get; set; }
    }
}
