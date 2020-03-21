using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStandard
{
    public delegate void StatusUpdate(string mode, string msg);

    public class Subject
    {

        public event StatusUpdate OnStatusUpdate = null;

        public void Attach(Observer obs)
        {
            OnStatusUpdate += new StatusUpdate(obs.Update);
        }

        public void Detach(Observer obs)
        {
            OnStatusUpdate -= new StatusUpdate(obs.Update);
        }

        public void Notify(string mode, string msg)
        {
            OnStatusUpdate?.Invoke(mode, msg);
        }
    }
}
