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
/// The View namespace.
/// </summary>
namespace WiredIn.Visualization.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class EmptyView.
    /// </summary>
    public partial class EmptyView : AbstractView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyView"/> class.
        /// </summary>
        public EmptyView()
        {
            InitializeComponent();
            this.viewName = "empty";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyView"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public EmptyView(IContainer container)
            : this()
        {
            container.Add(this);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetScore()
        {
            return -1;
        }

        /// <summary>
        /// Moves the view.
        /// </summary>
        /// <param name="g">if set to <c>true</c> [g].</param>
        public override void MoveView(bool g)
        {
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
        }

        #endregion Methods
    }
}