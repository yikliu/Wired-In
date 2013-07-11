using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using WiredIn.View;

namespace WiredIn.TransitionCommand
{
    public class FewStepTransitCommand : AbstractTransitionCommand
    {
        private Timer transitTimer;

        private Constants.State curState = Constants.State.Bad;

        enum DIRECTION { FromBadToGood, FromGoodToBad };

        private DIRECTION dir = DIRECTION.FromBadToGood;

        public FewStepTransitCommand(AbstractView view)
        {
           this.view = view;

           transitTimer = new Timer();         
           transitTimer.Elapsed += new ElapsedEventHandler(TransitionTimerTick);
        }

        private void TransitionTimerTick(Object sender, ElapsedEventArgs e)
        {           
            this.Transit();
        }

        public override void Transit()
        {
            if (this.curState == Constants.State.Bad)
            {
                this.view.UpdateView(false); //move the image to bad ones
            }
            else
            {
                this.view.UpdateView(true);//move the image to good ones. 
            }
        }
        /// <summary>
        ///  Generally, here rewarding policy is applied. When good becomes bad, wait for 5 minutes till image change,
        ///  when bad becomes good, wait only 10 seconds and change the image
        /// </summary>
        /// <param name="s"></param>
        public override void SetState(Constants.State s)
        {
            if (curState == Constants.State.Good)
            {
                if (this.curState != s)
                {
                    //good becomes bad, do something...
                    this.curState = Constants.State.Bad;
                    transitTimer.Stop();
                    transitTimer.Interval = 1000 * 10 ; //5 mins, 
                    transitTimer.Start();
                    dir = DIRECTION.FromGoodToBad;

                }
            }
            else
            {
                if (s == Constants.State.Good)
                {
                   //bad becomes good, do something...
                    this.curState = s;
                    transitTimer.Stop();
                    transitTimer.Interval = 1000 * 60 * 5; // wait 5 minutes to see they hold on to good state
                    transitTimer.Start();
                    dir = DIRECTION.FromBadToGood;
                }
            }                        
        }

        public override void SetUp() 
        {          
            
            transitTimer.Start();
        }

        public override void TearDown() 
        {
           
            transitTimer.Stop();
        }

       public override void Pause() 
       {
         
           transitTimer.Stop();
       }

       public override void Start()
       {
          
           transitTimer.Start();
       }
    }
}
