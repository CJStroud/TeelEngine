using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class RenderDisplay : GraphicsDeviceControl
    {

        public event EventHandler OnIntialize;
        public event EventHandler OnDraw;

        protected override void Initialize()
        {
            if (OnIntialize != null) OnIntialize(this, null);
        }

        protected override void Draw()
        {
            if (OnDraw != null) OnDraw(this, null);
        }
    }
}
