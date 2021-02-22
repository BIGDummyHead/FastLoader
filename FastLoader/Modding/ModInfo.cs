using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Valve.Newtonsoft.Json;

namespace FastandLow.Modding
{
    /// <summary>
    /// Info supplied to a mod
    /// </summary>
    public sealed class ModInfo
    {
        /// <summary>
        /// Name of the Mod
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the Author
        /// </summary>

        [JsonProperty("auth")]
        public string Author { get; set; }

        /// <summary>
        /// Description of the Mod
        /// </summary>

        [JsonProperty("desc")]
        public string Description { get; set; }

        /// <summary>
        /// Version of the Mod
        /// </summary>

        [JsonProperty("ver")]
        public string Version { get; set; }

        /// <summary>
        /// Directory of the Mod Executing
        /// </summary>
        public string Directory => Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// Should the Mod Load?
        /// </summary>
        [JsonProperty("load")]
        public bool Load { get; set; }

        /// <summary>
        /// No information was supplied, all values are set to "?" and <see cref="Load"/> is set to True
        /// </summary>
        public static ModInfo None => new ModInfo() { Author = "?", Load = true, Description = "?", Name = "?", Version = "?" };

    }
}
