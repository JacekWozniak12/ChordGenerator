using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    // {x}(Frequency}
    // sets frequency of note x;

    // [setting](value)
    // sets value of known setting

    // (x)
    // note x

    // (x + 7)
    // note being 7 semitones further than x

    // x ^ (x + 7)
    // note x and note being 7 semitones further than x

    // x ^ y
    // note x and note y

    // x + 5 ^ y - 2
    // note being 5 semitones further than x and
    // note being 2 semitones behing y

    public class SyntaxReader
    {
        public static SyntaxReader Instance {get; private set;}

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
            catch
            {

            }
        }

        public SyntaxReader()
        {
            Instance = this;
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
                    {

                    }
                }
                var z = 
                    RuntimeManager.Instance.MusicalNotes.
                    Find(y => y.Rank == RuntimeManager.
                    Instance.MusicalNotes.Find(x => x.Name == note).
                    Rank + modifier);
                notes.Add(z);
            }
            NAudioCommunication.Instance.PlaySound(notes.ToArray(), 0.1f, 0.5f, NAudio.Wave.SampleProviders.SignalGeneratorType.Sin);


        }

        /// <summary>
        /// Tries to read the single setting.
        /// </summary>
        public void ReadSetting(string input)
        {
            throw new NotImplementedException();
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