using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class WindowChangeActivity : Activity
    {
        public static DateTime LAST_WINDOW_CHANGE = DateTime.Now;
        
        String _newWinTitle = "";
        String _newProcName = "";

        public String NewWinTitle
        {
            get{return this._newWinTitle;}
            set { this._newWinTitle = value; }
        }

        public String NewProcName
        {
            get { return this._newProcName; }
            set { this._newProcName = value; }
        }

        public WindowChangeActivity(String p_name, String w_title, DateTime time):base(time)
        {
            _newProcName = p_name;
            _newWinTitle = w_title;
        }

        /*
         *	This calculates duration of previous activity
         */
        public override String getPreviousACDuration()
        {
            TimeSpan span = this.When() - LAST_WINDOW_CHANGE;
            LAST_WINDOW_CHANGE = this.When();
            return ", "+ span.TotalSeconds.ToString();
        }

        public override String What()
        {
            return "WC, " + _newProcName + ", " + _newWinTitle;
        }

        public override void Accept(Worker j)
        {
            j.CatchWindowChangeActivity(this);
        }
    }
}
