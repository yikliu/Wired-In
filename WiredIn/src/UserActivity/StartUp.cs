using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class StartUp : Activity
    {
        public StartUp(DateTime time, int s)
            : base(time,s)
        {            
        }

        public override String What()
        {
            return "StartUp";
        }

        public override void Accept(Worker j)
        {
            j.CatchStartUpActivity(this);
        }
    }
}
