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

    /// <summary>
    /// Get the unique id for this run (based on machine name and time)
    /// </summary>
    class RunIDKeeper
    {
        #region Fields

        /// <summary>
        /// The _instance
        /// </summary>
        public static RunIDKeeper _instance;

        /// <summary>
        /// The identifier
        /// </summary>
        private string id;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="RunIDKeeper"/> class from being created.
        /// </summary>
        private RunIDKeeper()
        {
            DateTime now = DateTime.Now;
            id = Environment.MachineName + "-"
                + now.Year.ToString() + "-"
                + now.Month.ToString() + "-"
                + now.Day.ToString() + "-"
                + now.Hour.ToString() + "-"
                + now.Minute.ToString();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the identifier keeper.
        /// </summary>
        /// <returns>RunIDKeeper.</returns>
        public static RunIDKeeper GetIDKeeper()
        {
            if (_instance == null)
            {
                _instance = new  RunIDKeeper();
            }

            return _instance;
        }

        /// <summary>
        /// Gets the run identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetRunID()
        {
            return id;
        }

        #endregion Methods
    }
}