using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    public class WindowChangeActivity : Activity
    {
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

        public override String What()
        {
            return "Window Changed to " + _newWinTitle + "Of Process: "+_newProcName;
        }

        public override void Accept(Worker j)
        {
            j.CatchWindowChangeActivity(this);
        }
    }
}
