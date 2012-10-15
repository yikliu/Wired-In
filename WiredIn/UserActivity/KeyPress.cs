using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Wired_In.Analyzer;

namespace Wired_In.UserActivity
{
    public class KeyPress : Activity
    {
        private Keys key_;
        public KeyPress(Keys key, DateTime time) : base(time)
        {
            key_ = key;
        }
        
        public override String What()
        {
            return  key_ + "KeyPressed";
        }

        public override void Accept(Judge j)
        {
            j.CatchKeyPressActivity(this);
        }
                
    }
}
