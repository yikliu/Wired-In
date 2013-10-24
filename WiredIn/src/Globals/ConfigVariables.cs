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
    using System.Windows.Forms;

    using WiredIn.DataStructure;

    /// <summary>
    /// Class ConfigVariables.
    /// </summary>
    public class ConfigVariables
    {
        #region Fields

        //A list of typical browser process names
        /// <summary>
        /// The browser proc names
        /// </summary>
        public readonly List<string> BrowserProcNames = new List<string>() { "firefox", "chrome", "iexplorer" };
        /// <summary>
        /// The dormant clock iteration
        /// </summary>
        public readonly int DormantClockIteration = 80; // Dormant set to 20s = 20 * 1000 / 250 = 80 iters
        /// <summary>
        /// The few step fast clock iteration
        /// </summary>
        public readonly int FewStepFastClockIteration = 720; // = 3 minutes = 3 * 60 * 1000 / 250 = 720 iters
        /// <summary>
        /// The few step slow clock iteration
        /// </summary>
        public readonly int FewStepSlowClockIteration = 1200; // = 5 minutes = 5 * 60 * 1000 / 250 = 1200 iters
        /// <summary>
        /// The global timer standard interval
        /// </summary>
        public readonly int GlobalTimerStandardInterval = 250;
        /// <summary>
        /// The logger buffer clock iteration
        /// </summary>
        public readonly int LoggerBufferClockIteration = 40; // = 10s, buffer log entry every 10s
        /// <summary>
        /// The many step fast clock iteration
        /// </summary>
        public readonly int ManyStepFastClockIteration = 2; // = 500s
        /// <summary>
        /// The many step slow clock iteration
        /// </summary>
        public readonly int ManyStepSlowClockIteration = 3; // = 750s
        /// <summary>
        /// The procrastination threshold iteration
        /// </summary>
        public readonly int ProcrastinationThresholdIteration = 2400; //10 minutes = 10 * 60 * 1000 / 250 = 2400 iters
        /// <summary>
        /// The shrink factor
        /// </summary>
        public readonly int ShrinkFactor = 5; // small screen is shrunk to factor of screen size;
        /// <summary>
        /// The standard steps
        /// </summary>
        public readonly int StandardSteps = 1000;

        //public readonly int ProcrastinationThresholdIteration = 40;
        /// <summary>
        /// The condition
        /// </summary>
        public OperandConditioning Condition = OperandConditioning.Reward;
        /// <summary>
        /// The debug image number
        /// </summary>
        public bool DebugImageNum = true;
        /// <summary>
        /// The enable SQL logging
        /// </summary>
        public bool EnableSQLLogging = false;
        /// <summary>
        /// The top most
        /// </summary>
        public bool TopMost = false;
        /// <summary>
        /// The window size
        /// </summary>
        public ApplicationSize WindowSize = ApplicationSize.Small;

       
        /// <summary>
        /// The folder for storing log informations and visual images
        /// </summary>
        public string WiredInFolder = Application.StartupPath;

        //make it singleton
        /// <summary>
        /// The _instance
        /// </summary>
        private static ConfigVariables _instance;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigVariables"/> class.
        /// </summary>
        protected ConfigVariables()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the configuration variables.
        /// </summary>
        /// <returns>ConfigVariables.</returns>
        public static ConfigVariables GetConfigVariables()
        {
            if (_instance == null)
            {
                _instance = new ConfigVariables();
            }
            return _instance;
        }

        #endregion Methods
    }
}