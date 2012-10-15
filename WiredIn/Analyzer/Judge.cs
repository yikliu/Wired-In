using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Wired_In.UserActivity;
using System.Collections.Specialized;
using System.Timers;

using Wired_In.Command;
using Wired_In.View;
using Wired_In.Constants;

namespace Wired_In.Analyzer
{
    public class Judge
    {
        private ObservableCollection<Activity> theActivityQueue;
        private TransitionCommand transitCommand;

        //private State theState;
        
        private Timer judgeTimer;

        private AbstractView view;

        private DateTime lastHitTime;

        private Boolean IsOnTask = false;

        public String StateString = "Off";

       

        public Judge(ObservableCollection<Activity> q,AbstractView v)
        {
            theActivityQueue = q;
            this.view = v;
            //theState = new State();
            judgeTimer = new Timer();
            
            judgeTimer.Interval = Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS;
            judgeTimer.Stop();
            judgeTimer.Elapsed += new ElapsedEventHandler(JudgeTimerTick);

            transitCommand = new NormalTransitCommand(this.view, false);
            lastHitTime = DateTime.Now;
        }

        public void setTimerInterval(int millSec)
        {
            judgeTimer.Interval = millSec;
        }

        public void StartJudge()
        {
            judgeTimer.Start();
            this.view.setUp();
            //theState.StartWatch();
        }

        public void StopJudge()
        {
            judgeTimer.Stop();
            this.view.tearDown();
            //theState.Clear();
        }

        public void PauseJudge()
        {
            judgeTimer.Stop();
            this.view.pause();
            //theState.PauseWatch();
        }

        /// <summary>
        ///   Called when a new activity is enqueued
        /// </summary>
        public void OnActiveQueueChange(Object sender,	NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems.Count == 1)
                {
                    Activity ac = theActivityQueue[e.NewStartingIndex];
                    ac.Accept(this);
                }
                else
                {
                    System.Console.WriteLine("More than one items added at once!");
                }
            }
        }

        
        
        private void JudgeTimerTick(Object sender, ElapsedEventArgs e)
        {           
            TimeSpan diff = DateTime.Now - lastHitTime;
            System.Console.WriteLine("State: "+ StateString);
            if (IsOnTask && diff.TotalSeconds >= Constants.Constants.DORMANT_INTERVAL_SECONDS)
            {
                if (this.judgeTimer.Interval != Constants.Constants.SLOW_UPDATE_RATE_MILLISECONDS)
                {
                    this.transitCommand.setDirection(false);
                    this.judgeTimer.Interval = Constants.Constants.SLOW_UPDATE_RATE_MILLISECONDS;
                    StateString = "Dormant";
                }

            }
            else if(IsOnTask)
            {
                if (this.judgeTimer.Interval != Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS)
                {
                    this.transitCommand.setDirection(true);
                    this.judgeTimer.Interval = Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS;
                    StateString = "Good";
                }
            }
            else
            {
                if (this.judgeTimer.Interval != Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS)
                {
                    this.transitCommand.setDirection(false);
                    this.judgeTimer.Interval = Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS;
                    StateString = "Bad";
                }
            }
            this.transitCommand.transit();            
        }

        public void CatchKeyPressActivity(KeyPress key_press_activity)
        {
            //AddKeyHit();
            lastHitTime = DateTime.Now;
        }

        public void CatchMouseClickActivity(MouseClick mouse_click_activity)
        {
            //AddMouseHit();
            lastHitTime = DateTime.Now;
        }

        private bool CheckOnOrOff(String proc_name)
        {
            if (proc_name.Equals("WINWORD"))
            {
                return true;
            }
            return false;
        }

        public void CatchProcChangeActivity(ProcChanged proc_change_activity)
        {
            bool b = CheckOnOrOff(proc_change_activity.NewProcName);
            if (IsOnTask != b)
            {
                IsOnTask = b;
                if (IsOnTask)
                {
                    this.transitCommand.setDirection(true);
                    StateString = "On";
                }
                else
                {
                    this.transitCommand.setDirection(false);
                    StateString = "Off";
                }
                this.judgeTimer.Interval = Constants.Constants.FAST_UPDATE_RATE_MILLISECONDS;
            }            
        }

        /*
        public double getCurrentScore()
        {
            
            double score = theState.getCurrentScore();
            if (score == 0.0)
            {
                System.Console.WriteLine("score is ZERO!");
                return 0.0001;
            }
            if (score == 1.0)
            {
                System.Console.WriteLine("score is ONE!");
                return 0.99999;
            }
            return score;
            

        }*/
       
    }
}
