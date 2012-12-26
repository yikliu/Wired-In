using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;


namespace WiredIn
{
    public class WiredInServer : MarshalByRefObject
    {
        private Dictionary<string, double> dic_client_score;

        public double getScore(string client_name)
        {
            return dic_client_score[client_name];
        }

        public void addClient(string name)
        {
            lock(this)
            {
                dic_client_score.Add(name, 0.0);
            }

        }

        public void removeClient(string name)
        {
            lock (this)
            {
                dic_client_score.Remove(name);
            }
        }

    }
}
