using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.UI.RadioButtonTile 
{
    public class ChosenVisualization : Subject
    {
        private Dictionary<string, bool> selected;

        public ChosenVisualization()
        {
            this.selected = new Dictionary<string, bool>();
        }

        public override void notify()
        {
            foreach (string k in observers.Keys)
            {
                observers[k].Update(selected[k]);
            }
        }

        public override void Register(string key, Observer o)
        {
            base.Register(key, o);
            selected.Add(key, false);
        }

        public override void UnregisterAll()
        {
            base.UnregisterAll();
            selected.Clear();
        }

        public void VisualizationSelected(String key)
        {
            string[] ks = selected.Keys.ToArray();
            foreach (string k in ks)
            {
                selected[k] = false;
            }
            selected[key] = true;
            notify();
        }

        public string GetSelectedKey()
        {
            string[] ks = selected.Keys.ToArray();
            foreach (string k in ks)
            {
                if (selected[k])
                {
                    return k;
                }
            }
            return null;
        }
    }
}
