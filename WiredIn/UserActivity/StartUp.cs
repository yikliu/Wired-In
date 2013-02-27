using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class StartUp : Activity
    {

        public StartUp(DateTime time)
            : base(time)
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
