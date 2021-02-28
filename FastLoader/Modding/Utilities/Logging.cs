using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// Log to the <see cref="Debug"/>
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// <see cref="Debug.Log(object)"/>
        /// </summary>
        /// <param name="message"></param>
        public static void Log(object message) => Debug.Log(message);

        /// <summary>
        /// <see cref="Debug.LogWarning(object)"/>
        /// </summary>
        /// <param name="warning"></param>
        public static void LogWarning(object warning) => Debug.LogWarning(warning);

        /// <summary>
        /// <see cref="Debug.LogError(object)"/>
        /// </summary>
        /// <param name="error"></param>
        public static void LogError(object error) => Debug.LogError(error);

        
    }
}
