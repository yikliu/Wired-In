using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wired_In.Command
{
    interface TransitionCommand
    {
        void transit();        
        void setDirection(bool d);
    }
}
