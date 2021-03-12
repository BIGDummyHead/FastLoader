using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using HarmonyLib;
using static FastandLow.Modding.Events.ModEventHandlers;

namespace FastandLow.Modding.Utilities
{

    /// <summary>
    /// Utilities to help you make a mod!
    /// </summary>
    public static class ModUtilities
    {
        /// <summary>
        /// Reloads All Mods For Your Game!
        /// </summary>
        public static void ReloadMods() => Bootstrap.Boot.ReloadMods();

        internal static void _patchGame()
        {
            new Harmony("com.FastandLow.Modding").PatchAll();
        }

        #region Registration
        private static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
        private static Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
        private static Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();

        #region Audio

        /// <summary>
        /// Register an Audio Clip for later
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileBaseDir">Base Directory</param>
        /// <param name="customName">A Custom Name To Be Accessed Under</param>
        public static void RegisterAudio(string fileName, string fileBaseDir, string customName = null)
        {
            string audioPath = Path.Combine(fileBaseDir, fileName);

            if (customName is null)
                customName = Path.GetFileNameWithoutExtension(audioPath);

            if (clips.ContainsKey(customName))
            {
                Console.WriteLine($"{customName} Has Already Been Registered!");
                return;
            }

            using (UnityWebRequest loaded = UnityWebRequestMultimedia.GetAudioClip(audioPath, GetAudioType(audioPath)))
            {
                loaded.SendWebRequest();

                while (!loaded.isDone)
                {

                }

                bool error = !string.IsNullOrEmpty(loaded.error);

                if (error)
                {
                    Console.WriteLine($"Failed To Load Your Audio! {customName}");
                    return;
                }

                AudioClip clip = DownloadHandlerAudioClip.GetContent(loaded);

                clips.Add(customName, clip);
            }
        }

        /// <summary>
        /// Get a Registered Audio Clip -> <see cref="RegisterAudio(string, string, string)"/>
        /// </summary>
        /// <param name="registeredName"></param>
        /// <returns></returns>
        public static AudioClip GetAudio(string registeredName)
        {
            if (clips.ContainsKey(registeredName))
            {
                return clips[registeredName];
            }

            Console.WriteLine($"Make Sure To Register {registeredName} Before Use");
            return default;
        }

        /// <summary>
        /// Get Audio From Your Registered Audio and Delegate an Action when it is loaded
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onLoad"></param>
        public static void GetAudio(string name, Action<AudioClip> onLoad)
        {
            if (clips.ContainsKey(name))
            {
                onLoad.Invoke(GetAudio(name));
            }
            else
                Console.WriteLine("Register Audio First " + name);
        }

        /// <summary>
        /// Pass in a File path and this will determine an <see cref="AudioType"/>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static AudioType GetAudioType(string filePath)
        {
            if (Path.HasExtension(filePath))
            {
                switch (Path.GetExtension(filePath))
                {
                    case ".wav":
                        return AudioType.WAV;
                    case ".mp3":
                        return AudioType.MPEG;
                    case ".mp2":
                        return AudioType.MPEG;
                    default:
                        break;
                }
            }
            return AudioType.UNKNOWN;
        }
        #endregion
        #region Bundles

        /// <summary>
        /// Register a Bundle 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileBaseDir">Base Directory</param>
        /// <param name="customName">A Custom Name To Accessed Under</param>
        public static void RegisterBundle(string fileName, string fileBaseDir, string customName = null)
        {
            string full = Path.Combine(fileBaseDir, fileName);

            if (customName is null)
                customName = fileName;

            if (bundles.ContainsKey(customName))
            {
                Console.WriteLine("This Bundle Is Already Registered " + customName);
                return;
            }

            AssetBundle bundle = AssetBundle.LoadFromFileAsync(full).assetBundle;

            bundles.Add(customName, bundle);
        }

        /// <summary>
        /// Get The <see cref="RegisterBundle(string, string, string)"/> as an <seealso cref="AssetBundle"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AssetBundle GetBundle(string name)
        {
            if (!bundles.ContainsKey(name))
            {
                Console.WriteLine("Please Register " + name);
                return default;
            }

            return bundles[name];
        }

        /// <summary>
        /// Get a Bundle and pass in an action to be used On Load
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onLoad"></param>
        public static void GetBundle(string name, Action<AssetBundle> onLoad)
        {
            if (bundles.ContainsKey(name))
                onLoad.Invoke(bundles[name]);
            else
                Console.WriteLine("Please Register " + name);
        }

        /// <summary>
        /// Get a Bundle
        /// </summary>
        /// <typeparam name="T">A <see cref="UnityEngine.Object"/></typeparam>
        /// <param name="name">The Name Of The Registered Bundle</param>
        /// <param name="instantiate">Instantiate The Bundle On Load - False Normally</param>
        /// <returns><typeparamref name="T"/></returns>
        public static T GetBundle<T>(string name, bool instantiate = false) where T : UnityEngine.Object
        {
            T ran = null;

            GetBundle(name, delegate (AssetBundle bundle)
            {
                T obj = bundle.LoadAsset<T>(bundle.GetAllAssetNames()[0]);

                if (!instantiate)
                    ran = obj;
                else
                    ran = UnityEngine.Object.Instantiate(obj);
            });

            return ran;
        }

        /// <summary>
        /// Get A GameObject from a bundle - Instantiates
        /// </summary>
        /// <param name="name">The name of the bundle</param>
        /// <param name="instantiate">Instantiate - Normally True</param>
        /// <returns>A <see cref="UnityEngine.GameObject"/></returns>
        public static GameObject GetBundle(string name, bool instantiate = true) => GetBundle<GameObject>(name, instantiate);

        #endregion
        #region Textures

        /// <summary>
        /// Register a Texture
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileBaseDir"></param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        /// <param name="customName">Custom name to be accessed by</param>
        public static void RegisterTexture(string fileName, string fileBaseDir, int width = 500, int height = 500, string customName = null)
        {
            string full = Path.Combine(fileBaseDir, fileName);

            if (customName is null)
                if (Path.HasExtension(full))
                    customName = Path.GetFileNameWithoutExtension(full);
                else
                    customName = fileName;

            if (images.ContainsKey(customName))
            {
                Console.WriteLine("This File Has Already Been Registered As An Image | " + customName);
                return;
            }

            Texture2D _text = new Texture2D(width, height, TextureFormat.RGBA32, false);

            if (_text.LoadImage(File.ReadAllBytes(full)))
            {
                images.Add(customName, _text);
                Console.WriteLine("Texture Registered " + customName);
            }
            else
                Console.WriteLine($"{customName} Could Not Be Registered");
        }

        /// <summary>
        /// Grab a Texture from your registered textures
        /// </summary>
        /// <param name="name"></param>
        /// <returns>an Image</returns>
        public static Texture2D GetTexture(string name)
        {
            if (images.ContainsKey(name))
                return images[name];
            else
                Console.WriteLine("Please Register " + name);

            return default;
        }
        #endregion

        #endregion
        #region Enemies and Players
        /// <summary>
        /// All Enemies
        /// </summary>
        public static GameObject[] Enemies => audioManager.instance.enemyList;

        /// <summary>
        /// Desktop Player
        /// </summary>
        public static GameObject Player => audioManager.instance.fpsPlayer;

        /// <summary>
        /// VR Player
        /// </summary>
        public static GameObject VRPlayer => audioManager.instance.vrPlayer;

        /// <summary>
        /// Are two players playing (locally)?
        /// </summary>
        public static bool LocalActivated => audioManager.instance.bothplayersActive;
        #endregion
        #region Events
        /// <summary>
        /// Invoked right before an Enemy Dies
        /// </summary>
        public static event EnemyDeathHandler BeforeEnemyDie;

        /// <summary>
        /// Invoked when an Enemy Dies
        /// </summary>
        public static event EnemyDeathHandler OnEnemyDie;

        [HarmonyPatch(typeof(basicAI), "Death", new Type[0])]
        internal static class OnEnemyDiePATCH
        {
            public static void Prefix(basicAI __instance)
            {
                BeforeEnemyDie?.Invoke(__instance.gameObject, __instance);
            }

            public static void Postfix(ref basicAI __instance)
            {
                OnEnemyDie?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// Invoked when a Civ Dies
        /// </summary>
        public static event CivilianDeathHandler OnCivilianDie;

        /// <summary>
        /// Invoked right before a Civ Dies
        /// </summary>
        public static event CivilianDeathHandler BeforeCivilianDie;


        [HarmonyPatch(typeof(civilianHp), "Die", new Type[0])]
        internal static class OnCivDiePATCH
        {
            public static void Prefix(civilianHp __instance)
            {
                BeforeCivilianDie?.Invoke(__instance.gameObject, __instance);
            }

            public static void Postfix(civilianHp __instance)
            {
                OnCivilianDie?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// Right when a civilian gets damaged
        /// </summary>
        public static event CivilianDamageHandler OnCivilianDamaged;

        [HarmonyPatch(typeof(civilianHp), "TakeDamageMain", new Type[] { typeof(float) })]
        internal static class OnCivDmgPATCH
        {
            public static void Prefix(civilianHp __instance, ref float amount)
            {
                OnCivilianDamaged?.Invoke(__instance.gameObject, __instance, ref amount);
            }
        }
        /// <summary>
        /// Right when an Enemy gets damaged
        /// </summary>
        public static event EnemyDamageHandler OnEnemyDamaged;

        [HarmonyPatch(typeof(enemyHp), "TakeDamageMain", new Type[] { typeof(float) })]
        internal static class OnEnemyDmgPATCH
        {
            public static void Prefix(enemyHp __instance, ref float amount)
            {
                OnEnemyDamaged?.Invoke(__instance.gameObject, __instance, ref amount);
            }
        }

        /// <summary>
        /// Right before an enemy spawns into the game
        /// </summary>
        public static event EnemySpawnHandler BeforeEnemySpawn;

        /// <summary>
        /// Right after an enemy spawns into the game
        /// </summary>
        public static event EnemySpawnHandler OnEnemySpawn;


        [HarmonyPatch(typeof(basicAI), "Awake", new Type[0])]
        internal static class OnEnemySpawnPATCH
        {
            public static void Prefix(basicAI __instance)
            {
                BeforeEnemySpawn?.Invoke(__instance.NPC.gameObject, __instance);
            }
            public static void Postfix(basicAI __instance)
            {
                OnEnemySpawn?.Invoke(__instance.NPC.gameObject, __instance);
            }
        }

        /// <summary>
        /// Right before a civilian spawns into the game
        /// </summary>
        public static event CivilianSpawnHandler BeforeCivilianSpawn;
        /// <summary>
        /// Right after a civilian spawns into the game
        /// </summary>
        public static event CivilianSpawnHandler OnCivilianSpawn;


        [HarmonyPatch(typeof(civilianAI), "Awake", new Type[0])]
        internal static class OnCivSpawnPATCH
        {
            public static void Prefix(civilianAI __instance)
            {
                BeforeCivilianSpawn?.Invoke(__instance.gameObject, __instance);
            }
            public static void Postfix(ref civilianAI __instance)
            {
                OnCivilianSpawn?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// Right when a FPS Player Spawns
        /// </summary>
        public static event PlayerSpawnHandler<fpsMovement> OnFPSPlayerSpawn;

        [HarmonyPatch(typeof(fpsMovement), "Awake", new Type[0])]
        internal static class OnFPSPlayerSpawnEvent
        {
            public static void Prefix(fpsMovement __instance)
            {
                OnFPSPlayerSpawn?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// Right when a VR player is added to the game
        /// </summary>
        public static event PlayerSpawnHandler<vrPlayerhealth> OnVRPlayerSpawn;

        [HarmonyPatch(typeof(vrPlayerhealth), "Awake", new Type[0])]
        internal static class OnVrPlayerSpawnEvent
        {
            public static void Prefix(vrPlayerhealth __instance)
            {
                OnVRPlayerSpawn?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// Right when a VR Player dies
        /// </summary>
        public static event PlayerDeathHandler<vrPlayerhealth> OnVRPlayerDie;

        [HarmonyPatch(typeof(vrPlayerhealth), "RPC_vrplayerDeath", new Type[] { typeof(int) })]
        internal static class OnVrPlayerDiePatch
        {
            public static void Prefix(vrPlayerhealth __instance)
            {
                OnVRPlayerDie?.Invoke(__instance.gameObject, __instance);
            }
        }

        /// <summary>
        /// When a Desktop Player Dies
        /// </summary>

        public static event PlayerDeathHandler<fpsMovement> OnFpsPlayerDie;

        [HarmonyPatch(typeof(fpsMovement), "RPC_fpsplayerDeath", new Type[] { typeof(int) })]
        internal static class OnFpsPlayerDiePatch
        {
            public static void Prefix(fpsMovement __instance)
            {
                OnFpsPlayerDie?.Invoke(__instance.gameObject, __instance);
            }
        }
        /// <summary>
        /// Right When a Desktop Player is damaged
        /// </summary>

        public static event PlayerDamageHandler<fpsMovement> OnFpsPlayerDamage;

        [HarmonyPatch(typeof(fpsMovement), "RPC_fpsHit", new Type[0])]
        internal static class OnFpsPlayerDmgPatch
        {
            public static void Prefix(fpsMovement __instance)
            {
                int d = 1;
                OnFpsPlayerDamage?.Invoke(__instance.gameObject, __instance, ref d, ref __instance.livesRem);
            }
        }

        [HarmonyPatch(typeof(fpsMovement), "takeExplosiondamage", new Type[] { typeof(int) })]
        internal static class OnFpsPlayerDmgPatch_Explosive
        {
            public static void Prefix(fpsMovement __instance, int amount)
            {
                OnFpsPlayerDamage?.Invoke(__instance.gameObject, __instance, ref amount, ref __instance.livesRem);
            }
        }

        /// <summary>
        /// Right when a VR player is damaged
        /// </summary>
        public static event PlayerDamageHandler<vrPlayerhealth> OnVRPlayerDamage;

        [HarmonyPatch(typeof(vrPlayerhealth), "takeDamage", new Type[0])]
        internal static class OnVRPlayerDmgPatch
        {
            public static void Prefix(vrPlayerhealth __instance)
            {
                int d = 1;
                OnVRPlayerDamage?.Invoke(__instance.gameObject, __instance, ref d, ref __instance.livesRem);
            }
        }

        [HarmonyPatch(typeof(vrPlayerhealth), "takeExplosiondamage", new Type[] { typeof(int) })]
        internal static class OnVRPlayerDmgPatch_Explosive
        {
            public static void Prefix(vrPlayerhealth __instance, int amount)
            {
                OnVRPlayerDamage?.Invoke(__instance.gameObject, __instance, ref amount, ref __instance.livesRem);
            }
        }

        #endregion

        /// <summary>
        /// Call For An Arrest of Enemies/Civilians
        /// </summary>
        /// <param name="caller">Where to call from</param>
        /// <param name="range">The range for how long the arrest is called, 30 being the base</param>
        public static void CallForArrest(Transform caller, float range = 30f)
        {
            Collider[] array = Physics.OverlapSphere(caller.position, range, 4096);
            foreach (Collider collider in array)
            {
                if (collider.transform.root.CompareTag("enemy") || collider.transform.root.CompareTag("civilian"))
                {
                    if (collider.transform.root.GetComponent<basicAI>() != null)
                    {
                        collider.transform.root.GetComponent<basicAI>().calledArrest(caller.gameObject);
                    }
                    if (collider.transform.root.GetComponent<civilianAI>() != null)
                    {
                        collider.transform.root.GetComponent<civilianAI>().calledArrest(caller.gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// Kills the selected Civilians
        /// </summary>
        /// <param name="civilian">The civilian to kill</param>
        public static void Kill(civilianHp civilian)
        {
            civilian.GetMethod("Die").CallMethod();
        }
    }
}
