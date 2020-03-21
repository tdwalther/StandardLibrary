using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStandard
{
    public delegate void RelayStatusUpdateDelegate(string mode, string msg);

    public class Observer
    {
        public RelayStatusUpdateDelegate OnRelayStatusUpdate = null;

        public virtual void Update(string mode, string msg)
        {
            OnRelayStatusUpdate?.Invoke(mode, msg);
        }
    }
}
