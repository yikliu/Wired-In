using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;

namespace WiredIn.UserActivity
{
    abstract public class Activity
    {
        DateTime time;
        public Activity(DateTime time)
        {
            this.time = time;
        }

        abstract public String What();

        public String When()
        {
            return this.time.ToShortDateString() +", "+ this.time.ToLongTimeString();
        }
       
        abstract public void Accept(Worker j);
    }
}
