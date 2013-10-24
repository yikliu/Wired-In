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
/// The Visualizer namespace.
/// </summary>
namespace WiredIn.Visualization.Visualizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WiredIn.DataStructure;
    using WiredIn.Visualization.Transit;
    using WiredIn.Visualization.View;

    /// <summary>
    /// Class AbstractVisualizer.
    /// </summary>
    public abstract class AbstractVisualizer : IRunner
    {
        #region Fields

        /// <summary>
        /// The transit
        /// </summary>
        protected AbstractTransit theTransit;
        /// <summary>
        /// The view
        /// </summary>
        protected AbstractView theView;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="s">The s.</param>
        public void ChangeState(Globals.State s)
        {
            this.theView.ChangeState(s);
            switch (s)
            {
                case Globals.State.Bad:
                    this.theTransit.ChangeToBadGear();
                    break;
                case Globals.State.Good:
                    this.theTransit.ChangeToGoodGear();
                    break;
                case Globals.State.Dormant:
                    this.theTransit.ChangeToDormantGear();
                    break;
            }
        }

        /// <summary>
        /// Gets the transit.
        /// </summary>
        /// <returns>AbstractTransit.</returns>
        public AbstractTransit GetTransit()
        {
            return this.theTransit;
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <returns>AbstractView.</returns>
        public AbstractView GetView()
        {
            return this.theView;
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return theTransit.IsRunning();
        }

        /// <summary>
        /// Sets the transit.
        /// </summary>
        /// <param name="t">The t.</param>
        public void SetTransit(AbstractTransit t)
        {
            this.theTransit = t;
            this.theTransit.LinkView(this.theView);  //link transit with view
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public void SetUp()
        {
            this.theView.SetUp();
            this.theTransit.SetUp();
        }

        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        public void SetView(AbstractView v)
        {
            this.theView = v;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.theTransit.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.theTransit.Stop();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public void TearDown()
        {
            this.theView.TearDown();
            this.theTransit.TearDown();
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int GetScore()
        {
            return this.theView.GetScore();
        }

        #endregion Methods
    }
}