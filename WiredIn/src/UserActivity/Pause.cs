using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.UserActivity
{
    public class Pause : Activity
    {
        public Pause(DateTime time, int s)
            : base(time,s)
        {
        }


        public override void Accept(Analyzer.Worker j)
        {
            j.CatchPauseActivity(this);
        }

        public override string What()
        {
            return "Pause";
        }
    }
}
