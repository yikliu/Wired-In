using System;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    /// <summary>
    /// Abstract class representing a user activity
    /// </summary>
    abstract public class Activity
    {
        DateTime time;
        int score;

        String state;

        public String StateString
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Activity(DateTime time, int s)
        {
            this.time = time;
            this.score = s;
        }

        public virtual int getScore()
        {
            return score;
        }

       bool catched = false;

       public bool Catched
       {
           get
           {
               return catched;
           }

           set
           {
               catched = value;
           }
       }
        
        abstract public String What();
       
        public virtual String getStats()
        {
            return "";
        }

        public DateTime When()
        {
            return time;
        }
        
        abstract public void Accept(Worker j);
    }
}
