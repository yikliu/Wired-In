using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.UserActivity
{
    public class MouseWheel : Activity
    {
        public MouseWheel(DateTime time, int s)
            : base(time,s)
        {
        }

        public override void Accept(Analyzer.Worker j)
        {
            j.CatchMouseWheelActivity(this);
        }

        public override string What()
        {
            return "Wheel";
        }
    }
}
