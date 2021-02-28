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
    public static class Boot
    {
        internal static Dictionary<string, Assembly> assembliesLoaded = new Dictionary<string, Assembly>();

        internal static ModInfo GetMod(string baseDir)
        {
            string modFile = Directory.GetFiles(baseDir).FirstOrDefault(x => x.Contains(".mod"));

            ModInfo _ret = ModInfo.None;

            try
            {
                _ret = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modFile));
            }
            catch
            {
            }

            //des mod JsonStruct

            return _ret;
        }


        internal static bool _loadedMods = false;

        internal static bool LoadedMods => _loadedMods;

        internal static void StartMods()
        {
            if (_loadedMods)
                return;

            Modding.Utilities.ModUtilities._patchGame();

            bool openConsole = JsonConvert.DeserializeObject<ConsoleLoader.Info>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\console.info")).OpenConsole;

            if (openConsole)
                ConsoleLoader.OpenConsole();

            string modsPath = Path.Combine(Directory.GetCurrentDirectory(), "Mods");
            int modsLoaded = 0;
            foreach (string dir in Directory.GetDirectories(modsPath))
            {
                IEnumerable<string> files = Directory.GetFiles(dir).Where(x => Path.GetExtension(x).Contains("dll"));

                foreach (string file in files)
                {
                    if (!assembliesLoaded.ContainsKey(file))
                    {
                        Assembly loaded = Assembly.LoadFrom(file);

                        assembliesLoaded.Add(file, loaded);

                        IEnumerable<Type> mods = loaded.GetTypes().Where(x => x.BaseType == typeof(Mod));

                        modsLoaded = LoadMods(mods, dir, file);
                    }
                }
            }

            FinishMods(modsLoaded);
        }

        internal static void ReloadMods()
        {
            foreach (KeyValuePair<string, Assembly> myAssems in assembliesLoaded)
            {
                string dir = Directory.GetParent(myAssems.Key).FullName;
                string file = myAssems.Key;

                IEnumerable<Type> mods = myAssems.Value.GetTypes().Where(x => x.BaseType == typeof(Mod));

                foreach (Type mod in mods)
                {
                    if (!mod.IsAbstract && mod.GetConstructor(new Type[] { }) != null)
                    {
                        Mod customMod = Activator.CreateInstance(mod) as Mod;
                        customMod.UnLoad();
                    }
                }

                int modsLoad = LoadMods(mods, dir, file);

                FinishMods(modsLoad);

            }
        }

        internal static int LoadMods(IEnumerable<Type> mods, string dir, string file)
        {
            int modsLoaded = 0;
            foreach (Type mod in mods)
            {
                if (!mod.IsAbstract && mod.GetConstructor(new Type[] { }) != null)
                {
                    Mod customMod = Activator.CreateInstance(mod) as Mod;

                    ModInfo info = GetMod(dir);

                    info.Directory = Directory.GetParent(file).FullName;

                    if (info.Compare(ModInfo.None))
                    {
                        Console.WriteLine($"Could Not Load Info File For Directory | {dir}");
                        customMod.Load(ModInfo.None);
                    }
                    else
                    {
                        if (info.Load)
                        {
                            Console.WriteLine($"{info.Name} Is Now Loading!");
                            customMod.Load(info);
                            modsLoaded++;
                        }
                        else
                        {
                            Console.WriteLine($"{info.Name} Will Not Be Loaded!");
                        }
                    }

                }
                else
                    Console.WriteLine("Mod Was Abstract, Cannot Make Instance | Mod Has Constructor That Cannot Be Resolved");
            }

            return modsLoaded;
        }

        private static void FinishMods(int number)
        {
            Console.WriteLine();
            Console.WriteLine($"Finished Loading {number} Mod(s)");
            Console.WriteLine("_________________________________");
            Console.WriteLine("This Loader was made by BIGDummyHead#8124");
            Console.WriteLine("_________________________________");
            Console.WriteLine();
            _loadedMods = true;
        }
    }
}
