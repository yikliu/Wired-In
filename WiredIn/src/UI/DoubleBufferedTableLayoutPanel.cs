using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn
{
    class DoubleBufferedTableLayoutPanel : System.Windows.Forms.TableLayoutPanel
    {
        public DoubleBufferedTableLayoutPanel()
        {
            DoubleBuffered = true;
        }
    }
}
