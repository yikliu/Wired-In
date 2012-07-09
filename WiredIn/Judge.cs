using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Wired_In.UserActivity;
using System.Collections.Specialized;
using System.Timers;


namespace Wired_In.Analyzer
{
    public class Judge
    {
        private ObservableCollection<Activity> theActivityQueue;

        private State theState;
        
        private Timer judgeTimer;

        public Judge(ObservableCollection<Activity> q)
        {
            theActivityQueue = q;
            theState = new State();
            judgeTimer = new Timer();
            
            judgeTimer.Interval = 1000;
            judgeTimer.Stop();
            judgeTimer.Elapsed += new ElapsedEventHandler(JudgeTimerTick);
        }

        public void StartJudge()
        {
            judgeTimer.Start();
            theState.StartWatch();
        }

        public void StopJudge()
        {
            judgeTimer.Stop();
            theState.Clear();
        }

        public void PauseJudge()
        {
            judgeTimer.Stop();
            theState.PauseWatch();
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
                    ac.Accept(theState);
                }
                else
                {
                    System.Console.WriteLine("More than one items added at once!");
                }
            }
        }

        private void JudgeTimerTick(Object sender, ElapsedEventArgs e)
        {
            theState.updateScore();            
        }

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
        }
       
    }
}
