using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    /// <summary>
    /// Abstract class representing a user activity
    /// </summary>
    abstract public class Activity
    {
        DateTime time;
        public Activity(DateTime time)
        {
            this.time = time;
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
       
        public virtual String getPreviousACDuration()
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
