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

    //Most common operations are in FewStepTransit
    /// <summary>
    /// Class ManyStepTransit
    /// </summary>
    class ManyStepTransit : FewStepTransit
    {
        #region Fields

        /// <summary>
        /// The configuration
        /// </summary>
        //private ConfigVariables config = ConfigVariables.GetConfigVariables();
        
        /// <summary>
        /// The current step
        /// </summary>
        private int currentStep = 0;
        /// <summary>
        /// The speed adjust factor
        /// </summary>
        private int speedAdjustFactor = 1;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the speed adjust factor.
        /// </summary>
        /// <value>The speed adjust factor.</value>
        public int SpeedAdjustFactor
        {
            get
            {
                return speedAdjustFactor;
            }
            set
            {
                speedAdjustFactor = value;
             }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether current curState [is in bad range].
        /// </summary>
        /// <returns><c>true</c> if [is in bad range]; otherwise, <c>false</c>.</returns>
        public bool IsInBadRange()
        {
            return currentStep <= 450;
        }

        /// <summary>
        /// Determines whether [is in good range].
        /// </summary>
        /// <returns><c>true</c> if [is in good range]; otherwise, <c>false</c>.</returns>
        public bool IsInGoodRange()
        {
            return currentStep >= 800;
        }

        /// <summary>
        /// Jumps curState to accelerate the feedback
        /// </summary>
        public void Jump()
        {
            if (config.Condition == Globals.OperandConditioning.Reward
                && IsInBadRange())
            {
                JumpFromBadRange();
            }
            else if (config.Condition == Globals.OperandConditioning.Punish
                && IsInGoodRange())
            {
                JumpFromGoodRange();
            }
        }

        /// <summary>
        /// Jumps from bad range.
        /// </summary>
        public void JumpFromBadRange()
        {
            if (IsInBadRange())
                currentStep = 600;
        }

        /// <summary>
        /// Jumps from good range.
        /// </summary>
        public void JumpFromGoodRange()
        {
            if (IsInGoodRange())
                currentStep = 700;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            this.SpeedAdjustFactor =  config.StandardSteps / view.GetCountOfSteps();
            base.SetUp();
        }

        /// <summary>
        /// Gets the fast iteration.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected override int GetFastIteration()
        {
            return config.ManyStepFastClockIteration * this.speedAdjustFactor;
        }

        /// <summary>
        /// Gets the slow iteration.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected override int GetSlowIteration()
        {
            return config.ManyStepSlowClockIteration * this.speedAdjustFactor;
        }

        /// <summary>
        /// Callback function for transition timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void TransitionTimerTick(Object sender, ElapsedEventArgs e)
        {
            this.Transit();
        }

        #endregion Methods
    }
}