using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine
{
    public class Keybind
    {
        public Keys Key{ get; set; }
        public Action Action { get; set; }

        public Keybind(Keys key, Action action)
        {
            Key = key;
            Action = action;
        }
    }
}
