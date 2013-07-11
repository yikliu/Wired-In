using System;
using System.Windows.Forms;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class KeyPress : Activity
    {
        private Keys key_;
        public KeyPress(Keys key, DateTime time, int s) : base(time,s)
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
