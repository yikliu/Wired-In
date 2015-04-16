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

namespace WiredIn.UserActivity
{
    using System;

    using WiredIn.Analyzer;

    /// <summary>
    /// Abstract class representing a user activity
    /// </summary>
    public abstract class Activity
    {
        #region Fields

        /// <summary>
        /// The catched
        /// </summary>
        bool catched = false;
        /// <summary>
        /// The score
        /// </summary>
        int score;
        /// <summary>
        /// The state
        /// </summary>
        String state;
        /// <summary>
        /// The time
        /// </summary>
        DateTime time;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="s">The s.</param>
        public Activity(DateTime time, int s)
        {
            this.time = time;
            this.score = s;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Activity"/> is catched.
        /// </summary>
        /// <value><c>true</c> if catched; otherwise, <c>false</c>.</value>
        public bool Catched
        {
            get
               {
               return catched;
               }

               set
               {
               catched = value;
               }
        }

        /// <summary>
        /// Gets or sets the state string.
        /// </summary>
        /// <value>The state string.</value>
        public String StateString
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Accepts the specified j.
        /// </summary>
        /// <param name="j">The j.</param>
        public abstract void Accept(Worker j);

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public virtual int getScore()
        {
            return score;
        }

        /// <summary>
        /// Gets the stats.
        /// </summary>
        /// <returns>String.</returns>
        public virtual String getStats()
        {
            return "";
        }

        /// <summary>
        /// Whats this instance.
        /// </summary>
        /// <returns>String.</returns>
        public abstract String What();

        /// <summary>
        /// Whens this instance.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime When()
        {
            return time;
        }

        #endregion Methods
    }
}