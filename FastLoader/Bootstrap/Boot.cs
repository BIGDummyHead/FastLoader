using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FastandLow.Modding;
using Valve.Newtonsoft.Json;

namespace FastandLow.Bootstrap
{
    /// <summary>
    /// The class that controls all mods
    /// </summary>
    public static class Boot
    {
        internal static Dictionary<string, Assembly> assembliesLoaded = new Dictionary<string, Assembly>();



        internal static bool _loadedMods = false;

        /// <summary>
        /// Have the mods been loaded?
        /// </summary>
        public static bool LoadedMods => _loadedMods;

        internal static ModLoader loader = new ModLoader(ModsFolder);

        /// <summary>
        /// The Mods Folder
        /// <see cref="Path.Combine(string, string)"/> -- <seealso cref="Directory.GetCurrentDirectory()"/>
        /// </summary>
        public static string ModsFolder => Path.Combine(Directory.GetCurrentDirectory(), "Mods");

        internal static void StartMods()
        {
            if (_loadedMods)
                return;

            Modding.Utilities.ModUtilities._patchGame();

            bool openConsole = JsonConvert.DeserializeObject<ConsoleLoader.Info>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\console.info")).OpenConsole;

            if (openConsole)
                ConsoleLoader.OpenConsole();


            NotifyUser(loader.LoadMods());
        }

        internal static void ReloadMods() => loader.ReloadMods();

        private static void NotifyUser(int number)
        {
            Console.WriteLine($"\r\nFinished Loading {number} Mod(s) \r\n_________________________________\r\n\r\nThis Loader was made by BIGDummyHead#8124\r\n_________________________________\r\n");
            _loadedMods = true;
        }
    }
}
