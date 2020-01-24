using ChordGenerator.Model;
using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    public class SyntaxReader
    {
        private readonly RuntimeManager rm;

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
                        rm.AddSound(a);
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Tries to read the single setting. TODO
        /// </summary>
        public Settings ReadSetting(string input)
        {
            return null;
            var part = input.Split('}');

            part[0] =
                part[0].Replace("{", "");
            part[1] =
                part[1].Trim('[').Trim(']');

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

                    case "duration":
                        break;

                    default:
                        throw new ArgumentException();
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

            List<Note> notes = new List<Note>();

            foreach (var item in s)
            {
                int t = 0;

                List<string> parts = new List<string>();
                parts.Add("");

                for (int i = 0; i < item.Length; i++)
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

                foreach (var part in parts)
                {
                    try
                    {
                        if (MusicalNote.IsValidName(part))
                        {
                            note = part;
                        }
                        else
                        {
                            if (int.TryParse(part, out int result))
                            {
                                modifier += result;
                            }
                            else throw new ArgumentException();
                        }
                    }
                    catch (ArgumentException e)
                    {
                        throw e;
                    }
                }
                var u = RuntimeManager.
                    Instance.RuntimeSettings.MusicalNotes.Find(x => x.Name == note);

                // Note tried to create MusicalNote because he has given frequency,
                // Issues with serialization TODO
                if (!(u.Frequency == 0))
                {
                    var z = rm.RuntimeSettings.MusicalNotes.Find(y => y.Rank == u.Rank + modifier);
                    Note o = new Note();
                    o.Name = z.Name;
                    notes.Add(o);
                }
            }
            return new Chord(notes.ToArray());
        }

        /// <summary>
        /// Tries to read the Note, next tries to change frequency of it and
        /// every single note in base.
        /// </summary>
        public void ReadNoteChange(string note, float frequency)
        {
            if (MusicalNote.IsValidName(note) && MusicalNote.IsValidFrequency(frequency))
            {
                rm.ChangeNoteArray(note, frequency);
            }
        }
    }
}