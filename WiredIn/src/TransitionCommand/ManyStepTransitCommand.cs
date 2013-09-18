using System;
using System.Timers;
using WiredIn.View;
using WiredIn.Constants;

namespace WiredIn.TransitionCommand
{
    class ManyStepTransitCommand : AbstractTransitionCommand
    {
       private Timer transitTimer; // Timer used to sync the image transition

        private bool goToGood = false;

        private int goodInterval = 0;
        private int badInterval = 0;
        private int dormantInterval = 0;

        private int currentStep = 0;
        private double speedAdjustFactor = 1;

        private SingletonConstant _constant = SingletonConstant.GetSingletonConstant();

        public double SpeedAdjustFactor
        {
            get { return speedAdjustFactor; }
            set { 
                speedAdjustFactor = value;
                InitializeIntervalBasedOnOperationalCondition();
                SetState(this.state);
            }
        }

        public override void SetState(Constants.State state)
        {
            this.state = state;
            switch (state)
            {
                case (Constants.State.Good):
                    SetTransitionTimerInterval(goodInterval);
                    goToGood = true;
                    break;
                case (Constants.State.Dormant):
                    SetTransitionTimerInterval(dormantInterval);
                    goToGood = false;
                    break;
                case (Constants.State.Bad):
                    SetTransitionTimerInterval(badInterval);
                    goToGood = false;
                    break;
                default:
                    break;
            }
        }
        
        public override void SetUp()
        {
            transitTimer.Start();
        }

        public ManyStepTransitCommand(AbstractView v)
        {
            this.view = v;
                        
            transitTimer = new Timer();         
            transitTimer.Stop();
            transitTimer.Elapsed += new ElapsedEventHandler(TransitionTimerTick);

            InitializeIntervalBasedOnOperationalCondition();
            SetState(Constants.State.Bad);
        }

        public void SetDirection(bool d)
        {
            this.goToGood = d;
        }

        public void setView(AbstractView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Initializes the interval based on operational condition.
        /// </summary>
        public void InitializeIntervalBasedOnOperationalCondition()
        {
            switch (_constant.Condition)
            {
                case Constants.OperandCondition.punish:
                    goodInterval = (int)(_constant.SlowUpdateRateInMilliSeconds * this.speedAdjustFactor);
                    badInterval = (int)(_constant.FastUpdateRateInMilliSeconds * this.speedAdjustFactor);
                    break;
                case Constants.OperandCondition.reward:
                    badInterval = (int)(_constant.SlowUpdateRateInMilliSeconds * this.speedAdjustFactor);
                    goodInterval = (int)(_constant.FastUpdateRateInMilliSeconds * this.speedAdjustFactor);
                    break;
            }
            dormantInterval = (int)(_constant.SlowUpdateRateInMilliSeconds * this.speedAdjustFactor);
        }

        /// <summary>
        /// Adjust the transition rate by setting timer interval
        /// </summary>
        /// <param name="millSec">timer interval in milliseconds</param>
        public void SetTransitionTimerInterval(int millSec)
        {
            transitTimer.Interval = millSec;
        }

        public virtual void Jump()
        {
            if (_constant.Condition == Constants.OperandCondition.reward 
                && IsInBadRange())
            {
                JumpFromBadRange();
            }
            else if (_constant.Condition == Constants.OperandCondition.punish 
                && IsInGoodRange())
            {
                JumpFromGoodRange();
            }
        }

        public bool IsInBadRange()
        {
            return currentStep <= 450;
        }

        public bool IsInGoodRange()
        {
            return currentStep >= 800;
        }

        public void JumpFromGoodRange()
        {
            if (IsInGoodRange())
                currentStep = 700;
        }

        public void JumpFromBadRange()
        {
            if (IsInBadRange())
                currentStep = 600;
        }

        /// <summary>
        /// Callback function for transition timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransitionTimerTick(Object sender, ElapsedEventArgs e)
        {           
            this.Transit();
        }

        public override void Transit()
        {
            view.UpdateView(goToGood);
        }

        override public void TearDown()
        {
            transitTimer.Stop();
        }

        override public void Pause()
        {
            transitTimer.Stop();
        }

        override public void Start()
        {
            transitTimer.Start();
        }
    }

    
}
