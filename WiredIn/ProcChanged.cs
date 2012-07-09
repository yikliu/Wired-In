using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wired_In.Analyzer;

namespace Wired_In.UserActivity
{
    public class ProcChanged : Activity
    {
        String _newProcName = "";
        public String NewProcName
        {
            get{return this._newProcName;}
            set { this._newProcName = value; }
        }

        public ProcChanged(String name, DateTime time):base(time)
        {
            _newProcName = name;
        }

        public override String What()
        {
            return "Process Changed to " + _newProcName;
        }

        public override void Accept(State theState)
        {
            theState.CatchProcChangeActivity(this);
        }
    }
}
