using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastandLow.Modding.Utilities.Events
{
    /// <summary>
    /// A base for ModEvents, Provides a nice base class for Instance etc...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModEventArgs<T> : EventArgs where T : Photon.Pun.MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">Instance of a type</param>
        /// <param name="args">Extra arguments you want to include</param>
        public ModEventArgs(T instance, object[] args)
        {
            Instance = instance;
            Arguments = args;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">Instance of a type</param>
        public ModEventArgs(T instance)
        {
            Instance = instance;
            Arguments = new object[0];
        }

        /// <summary>
        /// The instance
        /// </summary>
        public T Instance { get; }

        /// <summary>
        /// extra arguments
        /// </summary>
        public object[] Arguments { get; }
    }
}
