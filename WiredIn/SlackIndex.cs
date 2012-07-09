using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wired_In
{
    
    public abstract class SlackIndex
    {
        public abstract void updateScore(double x);
        public abstract void resetStartX(double y);
        public abstract double getCurrentScore();
        public abstract void setCurrentScore(double y);
        public abstract void setRate(double r);
    }

    public class EscalationSlackIndex : SlackIndex
    {
        private double score_;
        private double start_x;
        
        public double rate;

        public EscalationSlackIndex(double rate)
        {
            this.rate = rate;
        }

        public override void updateScore(double x)
        {
            score_ = 1 - Math.Exp(-rate * (start_x + x));
          
            if (Math.Abs(1 - score_) <= 0.0000001)
                score_ = 0.9999999;

            System.Console.WriteLine("Esca Score: " + score_);
        }

        public override void resetStartX(double y)
        {
            start_x = -Math.Log(1 - y) / rate;
        }

        public override double getCurrentScore()
        {
            return this.score_;
        }

        public override void setCurrentScore(double y)
        {
            this.score_ = y;
        }

        public override void setRate(double r)
        {
            this.rate = r;
        }

    }

    public class DeclineSlackIndex : SlackIndex
    {
        private double score_;
        private double start_x;
        
        public double rate;

        public DeclineSlackIndex(double rate)
        {
            this.rate = rate;
        }

        public override void updateScore(double x)
        {
            score_ = Math.Exp(-rate * (start_x + x));
            if (Math.Abs(score_ - 0) <= 0.0000001)
                score_ = 0.0000001;

            System.Console.WriteLine("Decline Score: " + score_);
        }

        public override void resetStartX(double y)
        {
            start_x = -Math.Log(y) / rate;
        }

        public override double getCurrentScore()
        {
            return this.score_;
        }

        public override void setCurrentScore(double y)
        {
            this.score_ = y;
        }

        public override void setRate(double r)
        {
            this.rate = r;
        }
 
    }
}
