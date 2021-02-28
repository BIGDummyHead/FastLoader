using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastandLow.Modding.Utilities.Events
{
    public class ModEventArgs<T> : EventArgs where T : Photon.Pun.MonoBehaviourPunCallbacks
    {
        public ModEventArgs(T instance, object[] args)
        {
            Instance = instance;
            Arguments = args;
        }

        public ModEventArgs(T instance)
        {
            Instance = instance;
            Arguments = new object[0];
        }

        public T Instance { get; }
        public object[] Arguments { get; }
    }
}
