using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class MouseWheel : Activity
    {
        public MouseWheel(DateTime time):base(time) { }
        public override String What()
        {
            return "Mouse Wheel Rolled!";
        }

        public override void Accept(Worker j)
        {
            
        }
    }
}
