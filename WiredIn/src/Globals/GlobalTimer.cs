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
/// The Globals namespace.
/// </summary>
namespace WiredIn.Globals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Timers;

    using WiredIn.DataStructure;

    /// <summary>
    /// This is global clock of application. It is used to sync all the time-based operations such as change images, 
    /// do logs, determine dormant and procrastination states.
    /// </summary>
    public class GlobalTimer : IRunner, IDisposable
    {
        #region Fields

        /// <summary>
        /// Singleton
        /// </summary>
        private static GlobalTimer _instance;

        /// <summary>
        /// The configuration
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The timer
        /// </summary>
        private Timer theTimer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="GlobalTimer"/> class from being created.
        /// </summary>
        private GlobalTimer()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the global timer.
        /// </summary>
        /// <returns>GlobalTimer.</returns>
        public static GlobalTimer GetGlobalTimer()
        {
            if (_instance == null)
            {
                _instance = new GlobalTimer();
            }
            return _instance;
        }

        /// <summary>
        /// Attaches the elapse event.
        /// </summary>
        /// <param name="newHandler">The new handler.</param>
        public void AttachElapseEvent(ElapsedEventHandler newHandler)
        {
            theTimer.Elapsed += newHandler;
        }

        /// <summary>
        /// Detaches the elapse event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void DetachElapseEvent(ElapsedEventHandler handler)
        {
            theTimer.Elapsed -= handler;
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return theTimer.Enabled;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public void SetUp()
        {
            if (theTimer == null)
            {
                theTimer = new Timer();
                theTimer.Interval = config.GlobalTimerStandardInterval;
            }
            theTimer.Enabled = false;
            theTimer.Stop();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!theTimer.Enabled)
                theTimer.Enabled = true;

            theTimer.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            theTimer.Stop();
            theTimer.Enabled = false;
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public void TearDown()
        {
            theTimer.Stop();
            theTimer.Close();
        }

        #endregion Methods

        /// <summary>
        /// Dispose Timer
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != theTimer)
                {
                    theTimer.Enabled = false;
                    theTimer.Dispose();
                    theTimer = null;
                }
            }
           
        }

    }
}