using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WiredIn.Globals;

namespace WiredIn.Visualization.Transit
{
    class ClockTransit : AbstractTransit
    {
        private bool goToGood = false;

        protected GlobalTimer globalTimer = GlobalTimer.GetGlobalTimer();

        public override void ChangeToBadGear()
        {
            goToGood = false;
        }

        public override void ChangeToDormantGear()
        {
            goToGood = false;
        }

        public override void ChangeToGoodGear()
        {
            goToGood = true;
        }

        public override void SetUp()
        {
            goToGood = false;
            globalTimer.AttachElapseEvent(this.Tick);
        }

        int curIter = 0;
        int oneSecondCount = 4;

        private void Tick(object sender, EventArgs e)
        {
            if (!is_running)
                return;

            if (curIter >= oneSecondCount)
            {
                this.Transit();
                curIter = 0;
            }
            else
            {
                curIter++;
            }
        }

        public override void Start()
        {
            is_running = true;
        }

        public override void Stop()
        {
            is_running = false;
        }

        public override void TearDown()
        {
            globalTimer.DetachElapseEvent(this.Tick);   
        }

        public override void Transit()
        {
            view.MoveView(goToGood);
        }
    }
}
