using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    public class SyntaxReader
    {        
        private RuntimeManager rm;

        public SyntaxReader()
        {
            rm = RuntimeManager.Instance;
        }

        /// <summary>
        /// Checks user input.
        /// Handled by GenerateButton_Click.
        /// </summary>
        public void ReadInput(string input)
        {
            try
            {
                input = input.Trim();
                switch (input[0])
                {
                    case '{':
                        var b = ReadSetting(input);
                        break;
                    default:
                        var a = ReadChord(input);
                        rm.PlaySound(a);
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Tries to read the single setting.
        /// </summary>
        public Settings ReadSetting(string input)
        {
            var part = input.Split('}');
            part[0] = part[0].Replace("{", "");
            part[1] = part[1].Trim('[').Trim(']');
            if (int.TryParse(part[1], out int result))
            {
                ReadNoteChange(part[0], result);
            }
            else
            {
                switch (part[0].ToLower())
                {
                    case "volume":
                        break;
                    case "playtype":
                        break;
                    case "singlenotetime":
                        break;
                    case "chordtime":
                        break;
                    default:
                        break;
                }
            }

            return new Settings();
        }

        /// <summary>
        /// Reads the Chord content
        /// </summary>
        public Chord ReadChord(string input)
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
                    rm.MusicalNotes.
                    Find(y => y.Rank == RuntimeManager.
                    Instance.MusicalNotes.Find(x => x.Name == note).
                    Rank + modifier);
                notes.Add(z);
            }           
            return new Chord(notes.ToArray());
        }

        /// <summary>
        /// Tries to read the Note, next tries to change frequency of it and
        /// every single note in base.
        /// </summary>
        public void ReadNoteChange(string Note, float frequency)
        {
            if (MusicalNote.IsValidName(Note) && MusicalNote.IsValidFrequency(frequency))
            {
                rm.GenerateMusicalNoteArray(Note, frequency);
            }
        }
    }
}