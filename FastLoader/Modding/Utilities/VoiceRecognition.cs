using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using System.Text;

namespace FastandLow.Modding.Utilities
{
    public sealed class VoiceRecognition
    {
        public Dictionary<string, Action> Commands { get; }
        public VoiceRecognition(Dictionary<string, Action> commands, ConfidenceLevel confidence = ConfidenceLevel.Low)
        {
            this.Commands = commands;

            KeywordRecognizer recog = new KeywordRecognizer(Commands.Keys.ToArray(), confidence);
            recog.OnPhraseRecognized += PhraseGotten;
            recog.Start();
        }

        private void PhraseGotten(PhraseRecognizedEventArgs args)
        {
            Action act;

            if (Commands.TryGetValue(args.text, out act))
                act.Invoke();
        }
    }

}
