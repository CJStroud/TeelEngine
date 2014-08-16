using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeelEngine.Level;

namespace TeelEngine.Render
{
    interface IAnimatable : IRenderable
    {
        Dictionary<string, int> Animations { get; set; }

        string CurrentAnimation { get; set; }

    }
}
