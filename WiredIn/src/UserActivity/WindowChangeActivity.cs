/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
/// <summary>
/// The UserActivity namespace.
/// </summary>
namespace WiredIn.UserActivity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WiredIn.Analyzer;
    using WiredIn.DataStructure;

    /// <summary>
    /// Class WindowChangeActivity.
    /// </summary>
    public class WindowChangeActivity : Activity
    {
        #region Fields

        /// <summary>
        /// The las t_ of f_ task
        /// </summary>
        public static DateTime LAST_OFF_TASK = DateTime.Now;
        /// <summary>
        /// The las t_ o n_ task
        /// </summary>
        public static DateTime LAST_ON_TASK = DateTime.Now;
        /// <summary>
        /// The las t_ state
        /// </summary>
        public static bool LAST_STATE = false;
        /// <summary>
        /// The las t_ windo w_ change
        /// </summary>
        public static DateTime LAST_WINDOW_CHANGE = DateTime.Now;

        /// <summary>
        /// The new window
        /// </summary>
        WindowInfo newWindow;
        /// <summary>
        /// The on off task
        /// </summary>
        TimeSpan onOffTask;
        /// <summary>
        /// The on one window
        /// </summary>
        TimeSpan onOneWindow;
        /// <summary>
        /// The on task
        /// </summary>
        TimeSpan onTask;
        /// <summary>
        /// The _is from off to on
        /// </summary>
        bool _isFromOffToOn = false;
        /// <summary>
        /// The _is from on to off
        /// </summary>
        bool _isFromOnToOff = false;
        /// <summary>
        /// The _is on
        /// </summary>
        bool _isOn = false;
        /// <summary>
        /// The _new proc name
        /// </summary>
        String _newProcName = "";
        /// <summary>
        /// The _new win title
        /// </summary>
        String _newWinTitle = "";

        #endregion Fields

        #region Constructors

        /*
        public WindowChangeActivity(String p_name, String w_title, DateTime time, int score):base(time, score)
        {
            _newProcName = p_name;
            _newWinTitle = w_title;
        }
        */
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowChangeActivity"/> class.
        /// </summary>
        /// <param name="newWindow">The new window.</param>
        /// <param name="time">The time.</param>
        /// <param name="score">The score.</param>
        public WindowChangeActivity(WindowInfo newWindow, DateTime time,  int score)
            : base(time, score)
        {
            this.newWindow = newWindow;
            this._newProcName = newWindow.ProcName;
            this._newWinTitle = newWindow.WinTitle;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is on.
        /// </summary>
        /// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Gets or sets the new name of the proc.
        /// </summary>
        /// <value>The new name of the proc.</value>
        public String NewProcName
        {
            get { return this._newProcName; }
            set { this._newProcName = value; }
        }

        /// <summary>
        /// Gets or sets the new window.
        /// </summary>
        /// <value>The new window.</value>
        public WindowInfo NewWindow
        {
            get { return newWindow; }
            set { newWindow = value; }
        }

        /// <summary>
        /// Gets or sets the new win title.
        /// </summary>
        /// <value>The new win title.</value>
        public String NewWinTitle
        {
            get { return this._newWinTitle;}
            set { this._newWinTitle = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Accepts the specified j.
        /// </summary>
        /// <param name="j">The j.</param>
        public override void Accept(Worker j)
        {
            j.CatchWindowChangeActivity(this);
        }

        /*
         *	This calculates duration of previous activity
         */
        /// <summary>
        /// Gets the stats.
        /// </summary>
        /// <returns>String.</returns>
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

        /// <summary>
        /// Whats this instance.
        /// </summary>
        /// <returns>String.</returns>
        public override String What()
        {
            return "WC, " + _newProcName + ", " + _newWinTitle.Replace(",","");
        }

        #endregion Methods
    }
}