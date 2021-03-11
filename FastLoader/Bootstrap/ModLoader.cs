using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastandLow.Modding;
using System.IO;
using System.Reflection;
using Valve.Newtonsoft.Json;

namespace FastandLow.Bootstrap
{
    internal sealed class ModLoader
    {
        //data on a mod
        internal struct Data
        {
            public Type Mod { get; set; }//the base class
            public string Directory { get; set; }//the directory the mod/modinfo is located
            public string DllFile { get; set; } //the AssemblyFile to load
        }

        internal struct LoadedMod
        {
            public Mod LoadedType { get; set; }
            public ModInfo Info { get; set; }
        }


        internal string ModsFolder { get; } //the mods folder

        internal ModLoader(string modsFolder)
        {
            this.ModsFolder = modsFolder;
        }

        internal Dictionary<string, Assembly> AssembliesLoaded { get; } = new Dictionary<string, Assembly>();
        internal List<LoadedMod> ModsLoaded { get; } = new List<LoadedMod>();

        internal IEnumerable<Data> Mods
        {
            get
            {
                foreach (string dir in Directory.GetDirectories(ModsFolder))
                {
                    IEnumerable<string> dllFiles = Directory.GetFiles(dir).Where(x => Path.GetExtension(x) == ".dll");

                    foreach (string dll in dllFiles)
                    {
                        if (!AssembliesLoaded.ContainsKey(dll))
                        {
                            Assembly loaded = Assembly.LoadFrom(dll);

                            IEnumerable<Type> modTypes = loaded.GetTypes().Where(x => x.BaseType == typeof(Mod));

                            AssembliesLoaded.Add(dll, loaded);

                            foreach (Type modType in modTypes)
                                yield return new Data { Directory = dir, DllFile = dll, Mod = modType };
                        }
                    }
                }
            }

        }

        internal void ReloadMods()
        {
            foreach (LoadedMod loaded in ModsLoaded)
            {
                Mod mod = loaded.LoadedType;

                mod.UnLoad();
                mod.Load(loaded.Info);
            }
        }

        internal int LoadMods()
        {
            int modsLoaded = 0;

            IEnumerable<Data> data = Mods;

            foreach (Data mod in data)
            {
                if (!mod.Mod.IsAbstract && mod.Mod.GetConstructor(new Type[] { }) != null)
                {
                    Mod customMod = Activator.CreateInstance(mod.Mod) as Mod;

                    ModInfo info = GetMod(mod.Directory);

                    info.Directory = Directory.GetParent(mod.DllFile).FullName;

                    if (info.Compare(ModInfo.None))
                    {
                        Console.WriteLine($"Could Not Load Info File For Directory | {mod.Directory}");
                        customMod.Load(ModInfo.None);
                    }
                    else
                    {
                        if (info.Load)
                        {
                            Console.WriteLine($"{info.Name} Is Now Loading!");

                            LoadedMod loadedM = new LoadedMod
                            {
                                LoadedType = customMod,
                                Info = info
                            };

                            if (!ModsLoaded.Contains(loadedM))
                            {
                                customMod.Load(info);
                                ModsLoaded.Add(loadedM);
                                modsLoaded++;
                            }
                            else
                                Console.WriteLine("Mod Has Already Been Initialized");


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

        private ModInfo GetMod(string baseDir)
        {
            string modFile = Directory.GetFiles(baseDir).FirstOrDefault(x => x.Contains(".mod"));

            ModInfo _ret = ModInfo.None;

            try
            {
                _ret = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modFile));
            }
            catch { }


            return _ret;
        }

    }
}
