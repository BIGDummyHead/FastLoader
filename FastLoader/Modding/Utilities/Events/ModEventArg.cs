using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastandLow.Modding.Utilities.Events
{
    public class ModEventArg<T> : EventArgs where T : Photon.Pun.MonoBehaviourPunCallbacks
    {
        public ModEventArg(T instance)
        {
            Instance = instance;
        }

        public T Instance { get; }
    }
}
