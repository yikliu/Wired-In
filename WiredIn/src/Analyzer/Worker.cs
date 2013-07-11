using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Timers;

using WiredIn.UserActivity;

using WiredIn.View;
using WiredIn.Log;
using WiredIn.Constants;
using WiredIn.TransitionCommand;

namespace WiredIn.Analyzer
{
    /// <summary>
    /// A worker is responsible for monitoring the activity queue and determining the transition direction
    /// and speed of the visualization
    /// </summary>
    public class Worker
    {
        private ObservableCollection<Activity> theActivityQueue;
       
        private Judge judge; //Judge attentive state

        private Logger logger; //Logger
       
        private Timer loggerTimer;

        private AbstractTransitionCommand transitCommand;
        
        public WiredIn.TransitionCommand.AbstractTransitionCommand TransitCommand
        {
            get { return transitCommand; }
            set { transitCommand = value; }
        }

        private DateTime lastHitTime;

        private Boolean IsOnTask = false;

        public Constants.State state = Constants.State.Bad;

        /// <summary>
        /// Worker constructor
        /// </summary>
        /// <param name="q">The queue storing all activities</param>
        /// <param name="v">View instance</param>
        public Worker(ObservableCollection<Activity> q, AbstractTransitionCommand t)
        {
            theActivityQueue = q;
            this.transitCommand = t;
            
            judge = new Judge();

            lastHitTime = DateTime.Now;

            logger = new Logger();
            loggerTimer = new Timer();
            loggerTimer.Interval = 1000; // Try to log every second
            loggerTimer.Elapsed += new ElapsedEventHandler(this.LoggerTimerTick);
        }


        /// <summary>
        /// Start worker
        /// </summary>
        public void StartWoker()
        {            
            loggerTimer.Start();
            this.transitCommand.SetUp();
        }

        /// <summary>
        /// Stop worker
        /// </summary>
        public void StopWorker()
        {            
            loggerTimer.Stop();
            this.transitCommand.TearDown();
            this.DequeueAll();
            logger.CloseFile();
        }

        /// <summary>
        /// Pause the judge for a moment 
        /// (disabled for the moment,  pausing will disrupt all the time diff calculation.)
        /// </summary>
        public void PauseJudge()
        {
            transitCommand.Pause();
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
    
        private void LoggerTimerTick(Object sender, ElapsedEventArgs e)
        {
            if (theActivityQueue.Count >= 1 && theActivityQueue[0].Catched)
            {
                lock (this)
                {
                    theActivityQueue.RemoveAt(0);
                }
            }
        }

        /// <summary>
        ///  Dump the remaining elements in activity queue before exiting
        /// </summary>
        private void DequeueAll()
        {
            while (theActivityQueue.Count > 0 && theActivityQueue[0].Catched)
            {
                theActivityQueue.RemoveAt(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key_press_activity"></param>
        /// <returns></returns>
        public void CatchKeyPressActivity(KeyPress key_press_activity)
        {
            key_press_activity.Catched = true;
            key_press_activity.StateString = this.state.ToString();
            lastHitTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouse_click_activity"></param>
        /// <returns></returns>
        public void CatchMouseClickActivity(MouseClick mouse_click_activity)
        {
            mouse_click_activity.Catched = true;
            mouse_click_activity.StateString = this.state.ToString();
            lastHitTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public void CatchStartUpActivity(StartUp su)
        {
            su.Catched = true;
            su.StateString = this.state.ToString(); ;
        }

        public void CatchShutDownActivity(ShutDown sd)
        {
            sd.Catched = true;
            sd.StateString = this.state.ToString();
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
                ac.IsON = IsOnTask;
                DetermineState();
            }
            ac.Catched = true;
            ac.StateString = this.state.ToString();
        }

        public void DriveView()
        {
            DetermineState();            
        }

        public void DetermineState()
        {
            TimeSpan diff = DateTime.Now - lastHitTime;

            if (IsOnTask && diff.TotalSeconds >= Constants.Config.DORMANT_INTERVAL_SECONDS)
            {
                UpdateTransitState(Constants.State.Dormant);
            }
            else if (IsOnTask)
            {
               UpdateTransitState(Constants.State.Good);
            }
            else
            {
                UpdateTransitState(Constants.State.Bad);
            }
        }

        private void UpdateTransitState(Constants.State s)
        {
            if (this.state != s)
            {
                this.state = s; 
                this.transitCommand.SetState(this.state);
            }
        }      
    }
}
