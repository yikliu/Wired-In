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
/// The Transit namespace.
/// </summary>
namespace WiredIn.Visualization.Transit
{
    using System.Timers;

    using WiredIn.DataStructure;
    using WiredIn.Visualization.View;

    /// <summary>
    /// Class AbstractTransit.
    /// </summary>
    public abstract class AbstractTransit : IRunner
    {
        #region Fields

        /// <summary>
        /// The is_running
        /// </summary>
        protected bool is_running = false;
        /// <summary>
        /// The view
        /// </summary>
        protected AbstractView view;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Changes to bad gear.
        /// </summary>
        public abstract void ChangeToBadGear();

        /// <summary>
        /// Changes to dormant gear.
        /// </summary>
        public abstract void ChangeToDormantGear();

        /// <summary>
        /// Changes to good gear.
        /// </summary>
        public abstract void ChangeToGoodGear();

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return this.is_running;
        }

        /// <summary>
        /// Links the view.
        /// </summary>
        /// <param name="v">The v.</param>
        public virtual void LinkView(AbstractView v)
        {
            this.view = v;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public abstract void SetUp();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Tears down.
        /// </summary>
        public abstract void TearDown();

        /// <summary>
        /// Transits this instance.
        /// </summary>
        public abstract void Transit();

        #endregion Methods
    }
}