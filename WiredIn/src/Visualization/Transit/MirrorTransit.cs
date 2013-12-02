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


    /// <summary>
    /// Class MirrorTransit.
    /// </summary>
    class MirrorTransit : AbstractTransit
    {
        private Globals.GlobalTimer globalTimer = Globals.GlobalTimer.GetGlobalTimer();
        
        private int currentIter = 0;
        private int TurnBadCount = 10;

        private bool goingToGood = false;

        #region Methods

        /// <summary>
        /// Changes to bad gear.
        /// </summary>
        public override void ChangeToBadGear()
        {
            goingToGood = false;
        }

        /// <summary>
        /// Changes to dormant gear.
        /// </summary>
        public override void ChangeToDormantGear()
        {
            goingToGood = false;
        }

        /// <summary>
        /// Changes to good gear.
        /// </summary>
        public override void ChangeToGoodGear()
        {
            goingToGood = true;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            globalTimer.AttachElapseEvent(this.TransitionTimerTick);
        }

        private void TransitionTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!is_running)
                return;

            if (goingToGood)
            {
                this.Transit();
            }
            else if (!goingToGood && currentIter >= TurnBadCount)
            {
                this.Transit();
                currentIter = 0;
            }
           else
            {
                currentIter++;
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
        }

        /// <summary>
        /// Transits this instance.
        /// </summary>
        public override void Transit()
        {
            view.MoveView(goingToGood);
        }

        #endregion Methods
    }
}