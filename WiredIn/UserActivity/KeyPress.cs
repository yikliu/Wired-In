using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
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
            return "K";
        }

        public override void Accept(Worker j)
        {
            j.CatchKeyPressActivity(this);
        }
                
    }
}
