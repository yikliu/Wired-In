using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.UI.RadioButtonTile
{
    public abstract class Subject
    {
        protected Dictionary<string, Observer> observers = new Dictionary<string, Observer>();

        public virtual void Register(string key, Observer o)
        {
            observers.Add(key, o);
        }

        public virtual void UnregisterAll()
        {
            observers.Clear();
        }

        public abstract void notify();       
    }

}
