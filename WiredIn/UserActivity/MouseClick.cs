using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
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

        public override void Accept(Worker j)
        {
            j.CatchMouseClickActivity(this);
        }
    }
}
