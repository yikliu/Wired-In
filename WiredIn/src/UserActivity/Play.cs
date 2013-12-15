using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.UserActivity
{
    public class Play : Activity
    {
        public Play(DateTime time, int s)
            : base(time,s)
        {
        }

        public override void Accept(Analyzer.Worker j)
        {
            j.CatchPlayActivity(this);
        }

        public override string What()
        {
            return "Play";
        }
    }
}
