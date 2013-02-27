using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Constants;

namespace WiredIn.Analyzer
{
    class Judge
    {
        public bool checkOnTask(String procName, String winTitle)
        {
            if (Constants.Config.WHITE_WIN.Contains(winTitle.ToLower()))
            {
                return true;
            }

            if(Constants.Config.WHITE_PROC.Contains(procName.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
