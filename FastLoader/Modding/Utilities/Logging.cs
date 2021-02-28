using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FastandLow.Modding.Utilities
{
    public static class Logging
    {
        public static void Log(object message) => Debug.Log(message);


        public static void LogWarning(object warning) => Debug.LogWarning(warning);

        public static void LogError(object error) => Debug.LogError(error);

        
    }
}
