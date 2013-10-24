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
/// The UserActivity namespace.
/// </summary>
namespace WiredIn.UserActivity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WiredIn.Analyzer;

    /// <summary>
    /// Class StartUp.
    /// </summary>
    public class StartUp : Activity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity" /> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="s">The s.</param>
        public StartUp(DateTime time, int s)
            : base(time,s)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Accepts the specified j.
        /// </summary>
        /// <param name="j">The j.</param>
        public override void Accept(Worker j)
        {
            j.CatchStartUpActivity(this);
        }

        /// <summary>
        /// Whats this instance.
        /// </summary>
        /// <returns>String.</returns>
        public override String What()
        {
            return "StartUp";
        }

        #endregion Methods
    }
}