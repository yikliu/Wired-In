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
#region Header

// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 06-28-2013
//
// Last Modified By : yikliu
// Last Modified On : 10-15-2012
// ***********************************************************************
// <summary></summary>
// ***********************************************************************

#endregion Header

/// <summary>
/// The DataStructure namespace.
/// </summary>
namespace WiredIn.DataStructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ManagedWinapi.Windows;

    /// <summary>
    /// Encapsulate some basic window information
    /// </summary>
    public class WindowInfo
    {
        #region Fields

        /// <summary>
        /// The proc identifier
        /// </summary>
        private int procID;
        /// <summary>
        /// The proc name
        /// </summary>
        private String procName;
        /// <summary>
        /// The win handle
        /// </summary>
        private IntPtr winHandle;
        /// <summary>
        /// The win title
        /// </summary>
        private String winTitle;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowInfo"/> class.
        /// </summary>
        /// <param name="win">The win.</param>
        public WindowInfo(SystemWindow win)
        {
            this.Update(win);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the proc identifier.
        /// </summary>
        /// <value>The proc identifier.</value>
        public int ProcID
        {
            get
            {
                return this.procID;
            }
        }

        /// <summary>
        /// Gets the name of the proc.
        /// </summary>
        /// <value>The name of the proc.</value>
        public String ProcName
        {
            get
            {
                return this.procName;
            }
        }

        /// <summary>
        /// Gets the window handle.
        /// </summary>
        /// <value>The window handle.</value>
        public IntPtr WindowHandle
        {
            get
            {
                return this.winHandle;
            }
        }

        /// <summary>
        /// Gets the win title.
        /// </summary>
        /// <value>The win title.</value>
        public String WinTitle
        {
            get
            {
                return this.winTitle;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Belongs to same process.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool BelongToSameProcess(WindowInfo info)
        {
            return this.procID == info.procID;
        }

        /// <summary>
        /// Belongs to same process.
        /// </summary>
        /// <param name="win">The win.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool BelongToSameProcess(SystemWindow win)
        {
            return this.procID == win.Process.Id;
        }

        /// <summary>
        /// Equalses the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(WindowInfo info)
        {
            return this.winHandle == info.winHandle;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.procName + ": "+ this.winTitle;
        }

        /// <summary>
        /// Updates the specified win.
        /// </summary>
        /// <param name="win">The win.</param>
        public void Update(SystemWindow win)
        {
            this.procID = win.Process.Id;
            this.procName = win.Process.ProcessName.ToLower();
            this.winTitle = win.Title.ToLower();
            this.winHandle = win.HWnd;
        }

        #endregion Methods
    }
}