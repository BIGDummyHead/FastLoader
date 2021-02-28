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
    public struct ModInfo
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
        public string Directory { get; set; }

        /// <summary>
        /// Should the Mod Load?
        /// </summary>
        [JsonProperty("load")]
        public bool Load { get; set; }

        /// <summary>
        /// No information was supplied, all values are set to "?" and <see cref="Load"/> is set to True
        /// </summary>
        public static ModInfo None => new ModInfo() { Author = "?", Load = true, Description = "?", Name = "?", Version = "?" };

        /// <summary>
        /// Compare to another ModInfo
        /// </summary>
        /// <param name="info"></param>
        /// <param name="compareDir"></param>
        /// <returns></returns>
        public bool Compare(ModInfo info, bool compareDir = false)
        {
            if (!compareDir)
                return this.Author == info.Author && this.Description == info.Description && this.Name == info.Name && this.Version == info.Version;

            return this.Author == info.Author && this.Description == info.Description && this.Name == info.Name && this.Version == info.Version && this.Directory == info.Directory;
        }
    }
}
