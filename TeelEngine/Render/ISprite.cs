using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TeelEngine
{
    public interface ISprite
    {
        Texture2D Texture { get; }
    }
}
