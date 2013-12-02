﻿/**
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
/// The Analyzer namespace.
/// </summary>
namespace WiredIn.Analyzer
{
    using System;
    using WiredIn.DataStructure;
    using WiredIn.Globals;

    /// <summary>
    /// Judge determines whether current topmost windows is on or off task
    /// </summary>
    public class Judge : IRunner
    {
        #region Fields

        /// <summary>
        /// const in TimeSpan construction. One milliseconds = 10,000 ticks. 
        /// </summary>
        private readonly int millisecondTick = 10000;

        /// <summary>
        /// The configuration instance
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        
        /// <summary>
        /// The current timer iteration, used for time the clock
        /// </summary>
        private int currentTimerIteration = 0;
        
        /// <summary>
        /// The current state
        /// </summary>
        private Globals.State curState = Globals.State.Bad;
        
        /// <summary>
        /// Get the global clock
        /// </summary>
        private GlobalTimer globalTimer = GlobalTimer.GetGlobalTimer();
        /// <summary>
        /// flag if this judge is running
        /// </summary>
        private bool is_running = false;
        /// <summary>
        /// A bool flag indicate whether current state is On or Off task, If "On", further analysis is needed to determine "dormant"
        /// </summary>
        private bool OnTask = false;
        /// <summary>
        /// The procrastinate iteration
        /// </summary>
        private int procrastinateIteration = 0;
        /// <summary>
        /// The work sphere instance
        /// </summary>
        private Worksphere workSphere = Worksphere.GetWorkSphere();

        #endregion Fields

        #region Delegates

        /// <summary>
        /// Delegate describes functions called when judge determines there is a state change.
        /// </summary>
        /// <param name="judge">The judge.</param>
        /// <param name="stateInfo">The <see cref="JudgeStateEventArgs" /> instance containing the event data.</param>
        public delegate void JudgeStateChangeHandler(object judge, JudgeStateEventArgs stateInfo);

        /// <summary>
        /// Delegate ProcrastinateEventHandler
        /// </summary>
        /// <param name="judge">The judge.</param>
        /// <param name="span">The TimeSpan spent on other things.</param>
        public delegate void ProcrastinateEventHandler(Object judge, ProcrastinateEvengArgs span);

        #endregion Delegates

        #region Events

        /// <summary>
        /// Occurs when judge determines there is a state change.
        /// </summary>
        public event JudgeStateChangeHandler OnJudgeStateChange;

        /// <summary>
        /// Occurs when procrastination is found
        /// </summary>
        public event ProcrastinateEventHandler OnProcrastinate;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public Globals.State CurState
        {
            get { return curState; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Checks whether certain window is on primary task or not, here only determines Good and Bad states
        /// </summary>
        /// <param name="newWinInfo">The new win information.</param>
        public void CheckOnTask(WindowInfo newWinInfo)
        {
            bool temp = IsProcABrowser(newWinInfo.ProcName) ?
                                           CheckKeyWords(newWinInfo.WinTitle) //if it's a browser, disregard handle, title or proc, just look at keywords matching
                                         : CheckWindows(newWinInfo); //if it's not a browser, then don't check title, only Check proc and handle normally

            if (OnTask != temp) //different than before
            {
                curState = temp ? Globals.State.Good : Globals.State.Bad;
                OnTask = temp;
                System.Console.WriteLine(curState.ToString());
                OnJudgeStateChange(this, new JudgeStateEventArgs(curState)); //Broadcast new state
                if (curState == Globals.State.Good)
                {
                    CheckProcrastinate();
                }
            }
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return is_running;
        }

        /// <summary>
        /// Receives a mouse/keyboard activity thus need to reset the dormant timer.
        /// </summary>
        public void ResetDormantTimer()
        {
            if (OnTask)
            {
                currentTimerIteration = 0;
                if(curState == Globals.State.Dormant)
                {
                    curState = Globals.State.Good;
                    System.Console.WriteLine("Good State!");
                    OnJudgeStateChange(this, new JudgeStateEventArgs(curState)); //notify the new dormant state
                    CheckProcrastinate();
                }
            }
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public void SetUp()
        {
            globalTimer.AttachElapseEvent(this.JudgeTimer_Elapse);
            is_running = false;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            is_running = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            is_running = false;
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public void TearDown()
        {
            globalTimer.DetachElapseEvent(this.JudgeTimer_Elapse);
            is_running = false;
        }

        //this is only called for browser process
        /// <summary>
        /// Checks the key words.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckKeyWords(String title)
        {
            title = title.ToLowerInvariant();
            int intersection = workSphere.SizeOfIntersectionWithKeywords(title);
            return intersection > 0;
        }

        /// <summary>
        /// Checks the procrastinate.
        /// </summary>
        private void CheckProcrastinate()
        {
            if (procrastinateIteration >= config.ProcrastinationThresholdIteration)
            {
                //how many ticks in this period of procrastination
                Int64 ticks = procrastinateIteration * config.GlobalTimerStandardInterval * millisecondTick;
                OnProcrastinate(this, new ProcrastinateEvengArgs(new TimeSpan(ticks)));
            }
        }

        //this is called by non-browser windows
        /// <summary>
        /// Checks the windows.
        /// </summary>
        /// <param name="curWin">The current win.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckWindows(WindowInfo curWin)
        {
            //if handle matches, return yes.
            if (workSphere.ContainsHandle(curWin.WindowHandle))
            {
                return true;
            }
            //if not, then check if it's a legit proc
            return workSphere.ContainsProcName(curWin.ProcName);
        }

        /// <summary>
        /// Determines whether this process is a browser.
        /// </summary>
        /// <param name="proc">The proc.</param>
        /// <returns><c>true</c> if [is proc a browser] [the specified proc]; otherwise, <c>false</c>.</returns>
        private bool IsProcABrowser(string proc)
        {
            foreach (string name in config.BrowserProcNames)
            {
                if (proc.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.WriteLine("Browser");
                    return true;
                }
            }
            System.Console.WriteLine("Not Browser");
            return false;
        }

        /// <summary>
        /// Handles the Elapse event of the JudgeTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void JudgeTimer_Elapse(object sender, EventArgs e)
        {
            if (!is_running)
                return;

            if (curState != Globals.State.Good)
            {
                procrastinateIteration++;
            }

            if (OnTask)
            {
                if (curState != Globals.State.Dormant && currentTimerIteration >= config.DormantClockIteration)
                {
                    curState = Globals.State.Dormant;
                    System.Console.WriteLine("Dormant State!");
                    OnJudgeStateChange(this, new JudgeStateEventArgs(curState)); //notify the new dormant state
                }
                currentTimerIteration++;
            }
        }

        #endregion Methods
    }

    /// <summary>
    /// Class JudgeStateEventArgs.
    /// </summary>
    public class JudgeStateEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// The new state
        /// </summary>
        public readonly Globals.State newState;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JudgeStateEventArgs" /> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public JudgeStateEventArgs(Globals.State s)
        {
            this.newState = s;
        }

        #endregion Constructors
    }

    /// <summary>
    /// Class ProcrastinateEvengArgs.
    /// </summary>
    public class ProcrastinateEvengArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// The span
        /// </summary>
        public readonly TimeSpan span;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcrastinateEvengArgs" /> class.
        /// </summary>
        /// <param name="sp">The sp.</param>
        public ProcrastinateEvengArgs(TimeSpan sp)
        {
            this.span = sp;
        }

        #endregion Constructors
    }
}