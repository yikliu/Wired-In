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
// Author           : Yikun Liu
// Created          : 09-25-2013
//
// Last Modified By : Yikun Liu
// Last Modified On : 09-25-2013
// ***********************************************************************
// <summary>
// A concrete view class for progress bar visualization
//</summary>
// ***********************************************************************

#endregion Header

/// <summary>
/// The View namespace.
/// </summary>
namespace WiredIn.Visualization.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    using WiredIn.Visualization.Transit;

    /// <summary>
    /// Class ProgressBarView
    /// </summary>
    public partial class ProgressBarView : AbstractView
    {
        #region Fields

        /// <summary>
        /// The current step
        /// </summary>
        int currentStep;
        /// <summary>
        /// The number of steps
        /// </summary>
        int numOfSteps;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarView" /> class.
        /// </summary>
        public ProgressBarView()
        {
            InitializeComponent();
            this.viewName = "progressbar";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetScore()
        {
            return currentStep;
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="g">if set to <c>true</c> [g].</param>
        public override void MoveView(bool g)
        {
            if (!g && currentStep >= 30)
            {
                currentStep--;
            }
            else if (g && (currentStep != numOfSteps))
            {
                currentStep++;
            }
            if (numOfSteps != 0)
            {
                double r = (double)currentStep / (double)numOfSteps;
                this.bar.Value = (int)(r * numOfSteps);
                System.Console.WriteLine(this.bar.Value);
            }
            this.bar.Invalidate();
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            numOfSteps = GetCountOfSteps();
            this.bar.Value = numOfSteps;
            currentStep = 2 * (numOfSteps / 3);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
        }

        /// <summary>
        /// Raises the <see cref="E:Resize" /> event.
        /// *center the bar
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (this.bar == null)
            {
                return;
            }
            this.bar.Width = (int)(this.ClientSize.Width * 0.8);

            if (this.bar.Width < this.ClientSize.Width)
            {
                this.bar.Left = (this.ClientSize.Width - this.bar.Width) / 2;
            }
            if (this.bar.Height < this.ClientSize.Height)
            {
                this.bar.Top = (this.ClientSize.Height - this.bar.Height) / 2;
            }
        }

        #endregion Methods
    }
}