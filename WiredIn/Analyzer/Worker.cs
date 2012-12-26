using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WiredIn.UserActivity;
using System.Collections.Specialized;
using System.Timers;

using WiredIn.Command;
using WiredIn.View;
using WiredIn.Constants;

namespace WiredIn.Analyzer
{
    public class Worker
    {
        private ObservableCollection<Activity> theActivityQueue;
        private TransitionCommand transitCommand;

        private Judge judge;
        
        private Timer judgeTimer;

        private AbstractView view;

        private DateTime lastHitTime;

        private Boolean IsOnTask = false;

        public String StateString = "Off";

        private int goodInterval = 0;
        private int badInterval = 0;
        private int dormantInterval = 0;

        public Worker(ObservableCollection<Activity> q,AbstractView v)
        {
            theActivityQueue = q;
            this.view = v;
            
            judgeTimer = new Timer();
            judge = new Judge();

            switch (Constants.Config.OPERAND_CONDITION)
            {
                case operant_condition.punish:
                    goodInterval = Constants.Config.SLOW_UPDATE_RATE_MILLISECONDS;
                    badInterval = Constants.Config.FAST_UPDATE_RATE_MILLISECONDS;
                    break;
                case operant_condition.reward:
                    badInterval = Constants.Config.SLOW_UPDATE_RATE_MILLISECONDS;
                    goodInterval = Constants.Config.FAST_UPDATE_RATE_MILLISECONDS;
                    break;
            }
            dormantInterval = Constants.Config.SLOW_UPDATE_RATE_MILLISECONDS;

            judgeTimer.Interval = badInterval;
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
            //System.Console.WriteLine("State: "+ StateString);
            if (IsOnTask && diff.TotalSeconds >= Constants.Config.DORMANT_INTERVAL_SECONDS)
            {
                if (this.judgeTimer.Interval != dormantInterval)
                {
                    this.transitCommand.setDirection(false);
                    this.judgeTimer.Interval = dormantInterval;
                    StateString = "Dormant";
                }
            }
            else if(IsOnTask)
            {
                if (this.judgeTimer.Interval != goodInterval)
                {
                    this.transitCommand.setDirection(true);
                    this.judgeTimer.Interval = goodInterval;
                    StateString = "Good";
                }
            }
            else
            {
                if (this.judgeTimer.Interval != badInterval)
                {
                    this.transitCommand.setDirection(false);
                    this.judgeTimer.Interval = badInterval;
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
        
        private bool CheckOnOrOff(String proc_name,String w_title)
        {
            return judge.checkOnTask(proc_name, w_title);
        }

        public void CatchWindowChangeActivity(WindowChangeActivity ac)
        {
            bool b = CheckOnOrOff(ac.NewProcName,ac.NewWinTitle);
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
                this.judgeTimer.Interval = Constants.Config.FAST_UPDATE_RATE_MILLISECONDS;
            }            
        }

        
    }
}
