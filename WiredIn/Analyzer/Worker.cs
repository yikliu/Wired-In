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

        private Judge judge; //Judge attentive state

        private Logger logger; //Logger
        
        private Timer transitTimer; // Timer used to sync the image transition
        private Timer loggerTimer;

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
            
            transitTimer = new Timer();
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

            transitTimer.Interval = badInterval;
            transitTimer.Stop();
            transitTimer.Elapsed += new ElapsedEventHandler(JudgeTimerTick);

            transitCommand = new NormalTransitCommand(this.view, false);
            lastHitTime = DateTime.Now;

            logger = new Logger();
            loggerTimer = new Timer();
            loggerTimer.Interval = 1000; // Try to log every second
            loggerTimer.Elapsed += new ElapsedEventHandler(this.LoggerTimerTick);
        }

        public void setTransitionTimerInterval(int millSec)
        {
            transitTimer.Interval = millSec;
        }

        public void StartWoker()
        {
            transitTimer.Start();
            loggerTimer.Start();
            this.view.setUp();
        }

        public void StopWorker()
        {
            transitTimer.Stop();
            loggerTimer.Stop();
            this.view.tearDown();
            this.DequeueAll();
            logger.CloseFile();
        }

        public void PauseJudge()
        {
            transitTimer.Stop();
            this.view.pause();
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
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems.Count == 1)
                {
                    Activity ac = (Activity)e.OldItems[0];
                    logger.Log(ac);
                }
                else
                {
                    System.Console.WriteLine("More than one items removed at once!");
                }
            }
        }        
        
        private void JudgeTimerTick(Object sender, ElapsedEventArgs e)
        {           
            TimeSpan diff = DateTime.Now - lastHitTime;
            //System.Console.WriteLine("State: "+ StateString);
            if (IsOnTask && diff.TotalSeconds >= Constants.Config.DORMANT_INTERVAL_SECONDS)
            {
                if (this.transitTimer.Interval != dormantInterval)
                {
                    this.transitCommand.setDirection(false);
                    this.transitTimer.Interval = dormantInterval;
                    StateString = "Dormant";
                }
            }
            else if(IsOnTask)
            {
                if (this.transitTimer.Interval != goodInterval)
                {
                    this.transitCommand.setDirection(true);
                    this.transitTimer.Interval = goodInterval;
                    StateString = "Good";
                }
            }
            else
            {
                if (this.transitTimer.Interval != badInterval)
                {
                    this.transitCommand.setDirection(false);
                    this.transitTimer.Interval = badInterval;
                    StateString = "Bad";
                }
            }
            this.transitCommand.transit();            
        }

        private void LoggerTimerTick(Object sender, ElapsedEventArgs e)
        {
            if (theActivityQueue.Count >= 1)
            {
                lock (this)
                {
                    theActivityQueue.RemoveAt(0);
                }
            }
        }

        private void DequeueAll()
        {
            while (theActivityQueue.Count > 0)
            {
                theActivityQueue.RemoveAt(0);
            }
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

        public void CatchStartUpActivity(StartUp su)
        {
        }

        public void CatchShutDownActivity(ShutDown sd)
        {
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
                this.transitTimer.Interval = Constants.Config.FAST_UPDATE_RATE_MILLISECONDS;
            }            
        }

        
    }
}
