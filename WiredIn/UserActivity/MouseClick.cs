using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class MouseClick : Activity
    {
        public MouseClick(DateTime time, int s):base(time,s)
        {            
        }

        public override String What()
        {
            return "M";
        }

        /************************************************************************/
        /* Visitor Pattern
         * Accept Method 
         */
        /************************************************************************/
        public override void Accept(Worker j)
        {
            j.CatchMouseClickActivity(this);
        }
    }
}
