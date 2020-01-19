using System;
using System.Collections.Generic;
using System.IO;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {
        public static RuntimeManager instance { get; private set; }

        public List<MusicalNote> musicalNotes { get; private set; }

        private string[] noteNameContent =
        {
            "C", "C#", "Db", "D", "D#",
            "Eb", "E", "F", "F#", "Gb",
            "G", "G#", "Ab", "A", "A#",
            "Bb", "B"
        };

        /// <summary>
        /// Handles startup
        /// </summary>
        public void InitializeApplication()
        {
            instance = this;
            GenerateMusicalNoteArray(16.35f);
        }

        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array. Uses the frequency for starting point
        /// </summary>
        private void GenerateMusicalNoteArray(float stratingFrequency)
        {
            if (musicalNotes == null)
            {
                musicalNotes = new List<MusicalNote>();
            }
            else musicalNotes.Clear();

            float temp = stratingFrequency;

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < noteNameContent.Length; j++)
                {
                    if (noteNameContent[j].Length == 1)
                    {
                        AddToMusicalNotes(new MusicalNote($"{noteNameContent[j]}{i}", temp));
                        temp = 25 * temp / 24;
                    }
                    else
                    {
                        if (noteNameContent[j][1] == '#')
                        {
                            AddToMusicalNotes(new MusicalNote($"{noteNameContent[j]}{i}", temp));
                            AddToMusicalNotes(new MusicalNote($"{noteNameContent[j + 1]}{i}", temp));
                            temp = 25 * temp / 24;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates the MusicalNote array from given note name and frequency.
        /// </summary>
        /// <throws>ArgumentException</throws>
        public void GenerateMusicalNoteArray(string note, float frequency)
        {
            try
            {
                var s = musicalNotes.Find(x => x.name == note).frequency;
                var b = musicalNotes[0].frequency;
                var d = s / b;
                frequency /= d;
                GenerateMusicalNoteArray(frequency);
            }
            catch (ArgumentException e)
            {
                //TODO Lukas -> funkcja wyświetlająca błąd
            }
        }

        // https://pages.mtu.edu/~suits/scales.html
        // https://pages.mtu.edu/~suits/chords.html

        private void AddToMusicalNotes(MusicalNote note)
        {
            if (musicalNotes == null)
            {
                musicalNotes = new List<MusicalNote>();
            }
            musicalNotes.Add(note);
        }

        /// <summary>
        /// Generates
        /// </summary>
        /// <returns>true if file is found</returns>
        public bool FindFileWithMusicalNoteArray()
        {
            // reads input output
            throw new NotImplementedException();

            try
            {
                return true;
            }
            catch (IOException e)
            {
            }
            return false;
        }

        public bool SaveMusicalNoteArrayToFile()
        {
            throw new NotImplementedException();
        }

        public bool FindFileWithSettings()
        {
            throw new NotImplementedException();
        }

        public void UseDefaultSettings()
        {
            throw new NotImplementedException();
        }

        /*public bool SaveUserSettings(Setting setting)
        {
        }*/

        public bool ChangeFrequencies(MusicalNote note)
        {
            throw new NotImplementedException();
        }
    }
}