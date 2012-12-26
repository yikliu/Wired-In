using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;


namespace WiredIn.View
{
    public partial class DigitalClockView : AbstractView
    {
        //private Stopwatch goodWatch;
        private Stopwatch badWatch;
        //private Stopwatch activeWatch;
        private System.Timers.Timer refreshTimer;
        private bool goToGood = false;

        public DigitalClockView()
        {
            InitializeComponent();
            //goodWatch = new Stopwatch();
            badWatch = new Stopwatch();
           
            //activeWatch = goodWatch;
            refreshTimer = new System.Timers.Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Elapsed += new ElapsedEventHandler(refreshTimerTick);
            
        }

        public override void setUp()
        {
             badWatch.Start();
             //goodWatch.Stop();
             //activeWatch = badWatch;
             refreshTimer.Start();
        }

        public override void tearDown() 
        {
            refreshTimer.Stop();
            badWatch.Stop();
            //activeWatch.Stop();
        }

        public override void pause() 
        {
            refreshTimer.Stop();
            badWatch.Stop();
            //activeWatch.Stop();
        }
        
        public override void updateView(bool g)
        {
            
            if(goToGood != g)
            {
                goToGood = g;
                //goodWatch.Reset();
                badWatch.Reset();
                if (goToGood)
                {
                    //goodWatch.Start();
                    badWatch.Stop();
                    digClock.DigitColor = System.Drawing.Color.Gray;                    
                }
                else
                {
                    badWatch.Start();
                    digClock.DigitColor = System.Drawing.Color.LimeGreen;
                    //goodWatch.Stop();
                    //activeWatch = badWatch;
                }
            }
        }

        private void refreshTimerTick(Object sender, ElapsedEventArgs e)
        {
            TimeSpan span = badWatch.Elapsed;
            if (span.Hours == 0)
            {
                digClock.DigitText = String.Format("{0}:{1}", span.Minutes.ToString("D2"), span.Seconds.ToString("D2"));
            }
            else
            {
                digClock.DigitText = String.Format("{0}:{1}:{2}",span.Hours, span.Minutes.ToString("D2"), span.Seconds.ToString("D2"));
            }
            
        }



    }
}
