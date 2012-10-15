using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wired_In.Analyzer;

namespace Wired_In.UserActivity
{
    public class MouseClick : Activity
    {
        public MouseClick(DateTime time):base(time)
        {            
        }

        public override String What()
        {
            return "MouseClicked!";
        }

        public override void Accept(Judge j)
        {
            j.CatchMouseClickActivity(this);
        }
    }
}
