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
    public static class Settings
    {
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

        public static void Write(string file, object item)
        {
            try
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        
    }
}
