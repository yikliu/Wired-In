using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WiredIn.UserActivity;

namespace WiredIn.Analyzer
{
    public  enum workstate { On, Off, dormant };
    public class State
    {
        protected int KeyHits;
        protected int MouseHits;

        private double SlowRate = 0.005;
        private double NormalRate = 0.01;

        public int CheckDormantInterval = 10; //During on state, check every this many second 
        public int DormainThreshold = 3; // if less than DormainThreshold hits in last CheckDormantInterval seconds, consider it dormant
        public int HitsInLastInterval = 0; // integer to record hits during last interval

        public workstate current_work_state = workstate.Off;
        
        protected int OnTotalHits;
        protected int OffTotalHits;

        public Stopwatch OnStateStopWatch;
        public Stopwatch OffStateStopWatch;
        public Stopwatch currentWatch;

        protected TimeSpan OnStateAccumulatedTime;
        protected TimeSpan OffStateAccumulatedTime;

        private EscalationSlackIndex escalation_index;
        private DeclineSlackIndex delince_index;

        private SlackIndex current_index;
       
        public State()
        {
            KeyHits = 0;
            MouseHits = 0;

            OnStateStopWatch = new Stopwatch();
            OffStateStopWatch = new Stopwatch();
            OnStateStopWatch.Stop();
            OffStateStopWatch.Stop();
            currentWatch = OffStateStopWatch;

            escalation_index = new EscalationSlackIndex(NormalRate);
            delince_index = new DeclineSlackIndex(NormalRate);
            current_index = escalation_index;
            
            OnStateAccumulatedTime = new TimeSpan();
            OffStateAccumulatedTime = new TimeSpan();
        }

        public void ChangeToOnState()
        {
            this.current_work_state = workstate.On;
            this.Reset();
            double y = current_index.getCurrentScore();
            current_index = delince_index;
            current_index.setRate(NormalRate);
            current_index.setCurrentScore(y);
            current_index.resetStartX(y);
            currentWatch = OnStateStopWatch;
            this.OnStateStopWatch.Start();
        }

        public void ChangeToOffState()
        {
            this.current_work_state = workstate.Off;
            this.Reset();
            double y = current_index.getCurrentScore();
            current_index = escalation_index;
            current_index.setRate(NormalRate);
            current_index.setCurrentScore(y);
            current_index.resetStartX(y);
            currentWatch = OffStateStopWatch;
            this.OffStateStopWatch.Start();
        }

        public void ChangeToDormantState()
        {
            this.current_work_state = workstate.dormant;
            this.Reset();
            double y = current_index.getCurrentScore();
            current_index = escalation_index;
            current_index.setRate(SlowRate);
            current_index.setCurrentScore(y);
            current_index.resetStartX(y);
            currentWatch = OffStateStopWatch;
            this.OffStateStopWatch.Start();
        }

        public void Reset()
        {
            OnStateAccumulatedTime += this.OnStateStopWatch.Elapsed;
            OffStateAccumulatedTime += this.OffStateStopWatch.Elapsed;

            this.OnStateStopWatch.Reset();
            this.OffStateStopWatch.Reset();
            
            KeyHits = 0;
            MouseHits = 0;
        }

        public void PauseWatch()
        {
            currentWatch.Stop();
        }

        public void AddKeyHit()
        {
            this.KeyHits++;
            HitsInLastInterval++;
        }

        public void AddMouseHit()
        {
            this.MouseHits++;
            HitsInLastInterval++;
        }

         public void StartWatch()
        {
            this.currentWatch.Start();
        }
           
        public void updateScore()
        {
            current_index.updateScore(currentWatch.Elapsed.TotalSeconds);

            if ((int)currentWatch.Elapsed.TotalSeconds % CheckDormantInterval != 0)
            {
                return;
            }

            if (this.current_work_state == workstate.On)
            {
                if (IsDormant())
                {
                    ChangeToDormantState();
                }
            }
            else if(this.current_work_state == workstate.dormant)
            {
                if (!IsDormant())
                {
                    ChangeToOnState();
                }
            }
            HitsInLastInterval = 0;
        }

        public bool IsDormant()
        {
            System.Console.WriteLine(HitsInLastInterval);
            if (this.HitsInLastInterval <= DormainThreshold)
                return true;
            else
                return false;
        }

        public double getCurrentScore()
        {
            double score = current_index.getCurrentScore();
            //System.Console.WriteLine("State: " + current_work_state);
            return score;
        }

        public void setCurrentScore(double y)
        {
            current_index.setCurrentScore(y);
        }

        public void ResetSlackIndexStartingX(double y)
        {
            current_index.resetStartX(y);
        }

        

       

        public void Clear()
        {
            OnStateAccumulatedTime = TimeSpan.Zero;
            OffStateAccumulatedTime = TimeSpan.Zero;

            this.delince_index.setCurrentScore(0);
            
            this.escalation_index.setCurrentScore(0);

            this.current_index = escalation_index;

            this.currentWatch = OffStateStopWatch;

            this.OnStateStopWatch.Reset();
            this.OffStateStopWatch.Reset();

            KeyHits = 0;
            MouseHits = 0;
        }
    }
}
