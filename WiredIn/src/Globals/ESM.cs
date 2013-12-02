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
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    using WiredIn.Globals;

    /// <summary>
    /// Class ESM.
    /// </summary>
    public class ESM
    {
        #region Fields

        /// <summary>
        /// The aware
        /// </summary>
        String aware;
        /// <summary>
        /// The reason
        /// </summary>
        String reason;
        /// <summary>
        /// The time
        /// </summary>
        string time;
        /// <summary>
        /// The visualization influence
        /// </summary>
        int visualizationInfluence = 0;
        /// <summary>
        /// The wasted
        /// </summary>
        TimeSpan wasted;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the aware.
        /// </summary>
        /// <value>The aware.</value>
        public String Aware
        {
            get { return aware; }
              set { aware = value; }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public String Reason
        {
            get { return reason; }
              set { reason = value; }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public String Time
        {
            get { return time; }
            set { this.time = value; }
        }

        /// <summary>
        /// Gets or sets the visualization influence.
        /// </summary>
        /// <value>The visualization influence.</value>
        public int VisualizationInfluence
        {
            get { return visualizationInfluence; }
              set { visualizationInfluence = value; }
        }

        /// <summary>
        /// Gets or sets the time wasted.
        /// </summary>
        /// <value>The wasted.</value>
        public TimeSpan Wasted
        {
            get { return wasted; }
            set { wasted = value; }
        }

        #endregion Properties
    }
}