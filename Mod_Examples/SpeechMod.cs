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

namespace SpeechMod
{
    public class SpeechMod : Mod
    {
        //vr or fps player
        GameObject inuse = null;

        //special arrest lines
        string[] ArrestLines;

        public override void Load(ModInfo info)
        {
            base.Load(info);

            //commands to be used 
            Dictionary<string, Action> commands = new Dictionary<string, Action>();

            //set arrest lines by using the settings.json file
            ArrestLines = Settings.GetValue<string[]>(Path.Combine(info.Directory, "settings.json"));

            //set each command to follow a different line but do the same action
            foreach (string command in ArrestLines)
                commands.Add(command, CallForArrest);

            //listen for keywords with my set of commands
            SpeechRecognizer recog = new SpeechRecognizer(commands);

            //manager
            MyManager manager = new GameObject("voiceManager").AddComponent<MyManager>();
            manager.reg = recog;
            //stop manager from being destroyed
            DontDestroyOnLoad(manager.gameObject);
            //when the fps player spawns or vr player - set the inuse gameobject - make sure the vr player get priority
            OnFPSPlayerSpawn += SpeechMod_OnFPSPlayerSpawn;
            OnVRPlayerSpawn += VRPlayerActive;
        }

        private void VRPlayerActive(object sender, FastandLow.Modding.Utilities.Events.ModEventArgs<vrPlayerhealth> e)
        {
            inuse = sender as GameObject;
            //notify that the current object being used is the vr character
            Console.WriteLine("Current Using VR For Voice");
        }

        private void SpeechMod_OnFPSPlayerSpawn(object sender, FastandLow.Modding.Utilities.Events.ModEventArgs<fpsMovement> e)
        {
            inuse = sender as GameObject;
            //notify that the current object being used is the fps character
            Console.WriteLine("Currently Using Fps For Voice");
        }

        public void CallForArrest()
        {
            //check if the player is null
            if (inuse != null)
            {
                //code from playerMovement
                    Collider[] array = Physics.OverlapSphere(inuse.transform.position, 30f, 4096);
                    foreach (Collider collider in array)
                    {
                        if (collider.transform.root.CompareTag("enemy") || collider.transform.root.CompareTag("civilian"))
                        {
                            if (collider.transform.root.GetComponent<basicAI>() != null)
                            {
                                collider.transform.root.GetComponent<basicAI>().calledArrest(inuse);
                            }
                            if (collider.transform.root.GetComponent<civilianAI>() != null)
                            {
                                collider.transform.root.GetComponent<civilianAI>().calledArrest(inuse);
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

        //start listening for keywords
        public void StartListener()
        {
            Recog.Start();
        }

        //stop listening for keywords
        public void StopListener() => Recog.Stop();

        //invoke command
        private void Recog_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            if (Commands.ContainsKey(args.text))
                Commands[args.text].Invoke();
        }
    }
}
