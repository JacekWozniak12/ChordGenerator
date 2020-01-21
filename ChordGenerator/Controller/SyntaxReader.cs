using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    public class SyntaxReader
    {
        public static SyntaxReader Instance { get; private set; }

        public void ReadInput(string input)
        {
            try
            {
                input = input.Trim();
                switch (input[0])
                {
                    case '{':
                        ReadSetting(input);
                        break;
                    default:
                        ReadChord(input);
                        break;
                }
            }
            catch { }
        }

        public SyntaxReader()
        {
            Instance = this;
        }

        /// <summary>
        /// Tries to read the single setting.
        /// </summary>
        public void ReadSetting(string input)
        {
            var part = input.Split('}');
            part[0].Replace("{", "");
            if (int.TryParse(part[1].Replace("[", "").Replace("]", ""), out int result))
            {
                ReadNoteChange(RuntimeManager.Instance, part[0], result);
            }
            else
            {
                switch (part[0].ToLower())
                {
                    case "volume":
                        break;
                    case "playtype":
                        break;
                    case "synth":
                        break;
                }
            }
        }

        /// <summary>
        /// Reads the Chord content
        /// </summary>
        public void ReadChord(string input)
        {
            input = input.Replace(" ", "");
            var s = input.Split('^');

            List<MusicalNote> notes = new List<MusicalNote>();

            foreach(var item in s)
            {
                int t = 0;

                List<string> parts = new List<string>();
                parts.Add("");

                for(int i = 0; i < item.Length; i++)
                {
                    if (item[i] == '-' || item[i] == '+')
                    {
                        parts.Add("");
                        t++;
                    }                   
                    parts[t] += item[i];
                }

                string note = "";
                int modifier = 0;

                foreach(var part in parts)
                {
                    try
                    {
                        if (MusicalNote.IsValidName(part))
                        {
                            note = part;
                        }
                        else
                        {
                            if(int.TryParse(part, out int result))
                            {
                                modifier += result;
                            }
                        }
                    }
                    catch
                    {}
                }
                var z = 
                    RuntimeManager.Instance.MusicalNotes.
                    Find(y => y.Rank == RuntimeManager.
                    Instance.MusicalNotes.Find(x => x.Name == note).
                    Rank + modifier);
                notes.Add(z);
            }

            // all at once
            // NAudioCommunication.Instance.PlaySound(notes.ToArray(), 0.1f, 0.5f, NAudio.Wave.SampleProviders.SignalGeneratorType.Sin);
            
            // one note after another
            foreach(var i in notes)
            {
                NAudioCommunication.Instance.PlaySound(new MusicalNote[] {i}, 0.1f, 0.5f, NAudio.Wave.SampleProviders.SignalGeneratorType.Sin);
            }


        }

        /// <summary>
        /// Tries to read the Note, next tries to change frequency of it and
        /// every single note in base.
        /// </summary>
        public void ReadNoteChange(RuntimeManager rm, string Note, float frequency)
        {
            if (MusicalNote.IsValidName(Note) && MusicalNote.IsValidFrequency(frequency))
            {
                rm.GenerateMusicalNoteArray(Note, frequency);
            }
        }
    }
}