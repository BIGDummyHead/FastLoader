using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastandLow.Modding;
using UnityEngine;
using UnityEngine.Windows.Speech;
using FastandLow.Modding.Utilities;
using static UnityEngine.Object;
using FastandLow.Modding.Serialization;
using static FastandLow.Modding.Utilities.ModUtilities;
using System.IO;
using FastandLow.Modding.Events;

namespace SpeechMod
{
    public class SpeechMod : Mod
    {
        GameObject characterInUse = null;

        //arrest lines
        string[] ArrestLines;

        public override void Load(ModInfo info)
        {
            base.Load(info);

            //commands to use
            Dictionary<string, Action> commands = new Dictionary<string, Action>();

            //Load settings from settings.json
            ArrestLines = Settings.GetValue<string[]>(Path.Combine(info.Directory, "settings.json"));

            //register each command, but have them use the same action
            foreach (string command in ArrestLines)
                commands.Add(command, CallForArrest);

            SpeechRecognizer recog = new SpeechRecognizer(commands);

            MyManager manager = new GameObject("voiceManager").AddComponent<MyManager>();
            manager.reg = recog;

            //never destroy the manager
            DontDestroyOnLoad(manager.gameObject);
            //log when the player is spawned
            OnFPSPlayerSpawn += FpsPlayerSpawned;
            OnVRPlayerSpawn += VRPlayerSpawned;
        }

        private void VRPlayerSpawned(GameObject sender, vrPlayerhealth instance)
        {
            //use this character for positioning
            characterInUse = sender;
            Console.WriteLine("Using VR Player For Voice");
        }

        private void FpsPlayerSpawned(GameObject sender, fpsMovement instance)
        {
            //use the desktop Player for positioning
            characterInUse = sender;
            Console.WriteLine("Using FPS Player For Voice");
        }



        public void CallForArrest()
        {
            //check if character has spawned?
            if (characterInUse != null)
            {
                //code from fpsMovement
                //                                                                               enemyMask
                Collider[] array = Physics.OverlapSphere(characterInUse.transform.position, 30f, 4096);
                foreach (Collider collider in array)
                {
                    if (collider.transform.root.CompareTag("enemy") || collider.transform.root.CompareTag("civilian"))
                    {
                        if (collider.transform.root.GetComponent<basicAI>() != null)
                        {
                            collider.transform.root.GetComponent<basicAI>().calledArrest(characterInUse);
                        }
                        if (collider.transform.root.GetComponent<civilianAI>() != null)
                        {
                            collider.transform.root.GetComponent<civilianAI>().calledArrest(characterInUse);
                        }
                    }
                }
            }
            else
                Console.WriteLine("Player Was Null");
        }
    }




    public class MyManager : MonoBehaviour
    {
        public SpeechRecognizer reg;

        private void Update()
        {
        }
    }

    public class SpeechRecognizer
    {
        public Dictionary<string, Action> Commands { get; private set; }
        public KeywordRecognizer Recog { get; private set; }
        public SpeechRecognizer(Dictionary<string, Action> actions, ConfidenceLevel level = ConfidenceLevel.Low)
        {
            this.Commands = actions;

            Recog = new KeywordRecognizer(actions.Keys.ToArray(), level);

            Recog.OnPhraseRecognized += Recog_OnPhraseRecognized;

            Recog.Start();
        }

        public void StartListener()
        {
            Recog.Start();
        }

        public void StopListener() => Recog.Stop();


        private void Recog_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            //invoke for keywords recognized, invoked CallForArrest - Line 66
            if (Commands.ContainsKey(args.text))
                Commands[args.text].Invoke();
        }
    }
}
