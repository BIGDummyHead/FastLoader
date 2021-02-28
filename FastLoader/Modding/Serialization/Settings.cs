using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Valve.Newtonsoft.Json;
using UnityEngine;
using FastandLow.Modding.Utilities;

namespace FastandLow.Modding.Serialization
{
    /// <summary>
    /// Settings 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Get a Value from a Json type file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingsFile">The Settings File</param>
        /// <returns></returns>
        public static T GetValue<T>(string settingsFile)
        {
            string json = File.ReadAllText(settingsFile);

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            return default;
        }

        /// <summary>
        /// Write an Object as Json to a <paramref name="file"/>
        /// </summary>
        /// <param name="file">The Json File</param>
        /// <param name="item"></param>
        public static void Write(string file, object item)
        {
            try
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        
    }
}
