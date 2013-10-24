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
    using System;
    using System.Timers;

    using WiredIn.Globals;
    using WiredIn.Visualization.View;

    /// <summary>
    /// Class FewStepTransit.
    /// </summary>
    public class FewStepTransit : AbstractTransit
    {
        #region Fields

        /// <summary>
        /// The active clock iteration
        /// </summary>
        protected int activeClockIteration = 0;
        /// <summary>
        /// The bad clock iteration
        /// </summary>
        protected int badClockIteration = 0;
        /// <summary>
        /// The configuration
        /// </summary>
        protected ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The current iter
        /// </summary>
        protected int currentIter = 0;
        /// <summary>
        /// The dormant clock iteration
        /// </summary>
        protected int dormantClockIteration = 0;
        /// <summary>
        /// The global timer
        /// </summary>
        protected GlobalTimer globalTimer = GlobalTimer.GetGlobalTimer();
        /// <summary>
        /// The good clock iteration
        /// </summary>
        protected int goodClockIteration = 0;
        /// <summary>
        /// The go to good
        /// </summary>
        protected bool goToGood = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FewStepTransit"/> class.
        /// </summary>
        public FewStepTransit()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Changes to bad gear.
        /// </summary>
        public override void ChangeToBadGear()
        {
            activeClockIteration = badClockIteration;
            goToGood = false;
        }

        /// <summary>
        /// Changes to dormant gear.
        /// </summary>
        public override void ChangeToDormantGear()
        {
            activeClockIteration = dormantClockIteration;
            goToGood = false;
        }

        /// <summary>
        /// Changes to good gear.
        /// </summary>
        public override void ChangeToGoodGear()
        {
            activeClockIteration = goodClockIteration;
            goToGood = true;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            InitializeIntervalBasedOnConditioning();
            ChangeToBadGear();
            globalTimer.AttachElapseEvent(this.TransitionTimerTick);
            is_running = false;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            is_running = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            is_running = false;
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
            globalTimer.DetachElapseEvent(this.TransitionTimerTick);
            is_running = false;
        }

        /// <summary>
        /// Transits this instance.
        /// </summary>
        public override void Transit()
        {
            view.MoveView(goToGood);
        }

        /// <summary>
        /// Gets the fast iteration.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected virtual int GetFastIteration()
        {
            return config.FewStepFastClockIteration;
        }

        /// <summary>
        /// Gets the slow iteration.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected virtual int GetSlowIteration()
        {
            return config.FewStepSlowClockIteration;
        }

        /// <summary>
        /// Initializes the interval based on conditioning.
        /// </summary>
        protected void InitializeIntervalBasedOnConditioning()
        {
            switch (config.Condition)
            {
                case Globals.OperandConditioning.Punish:
                    goodClockIteration = this.GetSlowIteration();
                    badClockIteration = this.GetFastIteration();
                    break;
                case Globals.OperandConditioning.Reward:
                    goodClockIteration = this.GetFastIteration();
                    badClockIteration = this.GetSlowIteration();
                    break;
            }
            dormantClockIteration = this.GetSlowIteration();
        }

        /// <summary>
        /// Transitions the timer tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void TransitionTimerTick(Object sender, ElapsedEventArgs e)
        {
            if (!is_running)
                return;

            if (currentIter >= activeClockIteration)
            {
                this.Transit();
                currentIter = 0;
            }
            else
            {
                currentIter++;
            }
        }

        #endregion Methods
    }
}