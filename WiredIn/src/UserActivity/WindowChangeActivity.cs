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
        public static DateTime LAST_ON_TASK = DateTime.Now;
        public static DateTime LAST_OFF_TASK = DateTime.Now;

        public static bool LAST_STATE = false;

        String _newWinTitle = "";
        String _newProcName = "";

        bool _isFromOffToOn = false;
        bool _isFromOnToOff = false;

        bool _isOn = false;

        TimeSpan onOneWindow;
        TimeSpan onTask;
        TimeSpan onOffTask;

        WindowInfo newWindow;

        public WindowInfo NewWindow
        {
            get { return newWindow; }
            set { newWindow = value; }
        }

        public bool IsON
        {
            get { return this._isOn; }
            set { 
                this._isOn = value;
                if (LAST_STATE != _isOn)
                {
                    if (_isOn)
                    {
                        _isFromOffToOn = true;
                        _isFromOnToOff = false;
                    }
                    else
                    {
                        _isFromOnToOff = true;
                        _isFromOffToOn = false;
                    }
                    LAST_STATE = _isOn;
                }
                else //LAST_STATE = _isON
                {
                    if (_isFromOffToOn)
                        _isFromOffToOn = false;
                    if (_isFromOnToOff)
                        _isFromOnToOff = false;
                }                
            }
        }

        public String NewWinTitle
        {
            get { return this._newWinTitle;}
            set { this._newWinTitle = value; }
        }

        public String NewProcName
        {
            get { return this._newProcName; }
            set { this._newProcName = value; }
        }

        /*
        public WindowChangeActivity(String p_name, String w_title, DateTime time, int score):base(time, score)
        {
            _newProcName = p_name;
            _newWinTitle = w_title;
        }
        */
        public WindowChangeActivity(WindowInfo newWindow, DateTime time,  int score)
            : base(time, score)
        {
            this.newWindow = newWindow;
            this._newProcName = newWindow.ProcName;
            this._newWinTitle = newWindow.WinTitle;
        }

        /*
         *	This calculates duration of previous activity
         */
        public override String getStats()
        {
            StringBuilder sb = new StringBuilder();
            onOneWindow = this.When() - LAST_WINDOW_CHANGE;
            LAST_WINDOW_CHANGE = this.When();
            sb.Append(",");
            sb.Append(onOneWindow.TotalSeconds.ToString());
            
            if(_isFromOffToOn)
            {
                onOffTask = this.When() - LAST_OFF_TASK;
                LAST_ON_TASK= this.When();
                sb.Append(",");
                sb.Append("OffTime:" + onOffTask.TotalSeconds.ToString());
            }

            if (_isFromOnToOff)
            {
                onTask = this.When() - LAST_ON_TASK;
                LAST_OFF_TASK = this.When();
                sb.Append(",");
                sb.Append("OnTime:" + onTask.TotalSeconds.ToString());
            }
            return sb.ToString();
        }

        public override String What()
        {
            return "WC, " + _newProcName + ", " + _newWinTitle.Replace(",","");
        }

        public override void Accept(Worker j)
        {
            j.CatchWindowChangeActivity(this);
        }
    }
}
