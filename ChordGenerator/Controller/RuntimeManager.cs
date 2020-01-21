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
        public static RuntimeManager Instance { get; private set; }

        public List<Chord> ChordsPlayed = new List<Chord>();
        public List<MusicalNote> MusicalNotes { get; private set; }

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
        public RuntimeManager()
        {
            Instance = this;
            GenerateMusicalNoteArray(16.35f);
        }

        public void PlaySound(Chord chord)
        {
            try
            {
                NAudioCommunication.
                    Instance.
                    PlaySound(chord, 0.5f, 0.5f,
                        NAudio.Wave.SampleProviders.SignalGeneratorType.Sin);

                ChordsPlayed.Add(chord);
            }
            catch { }
        }

        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array. Uses the frequency for starting point
        /// </summary>
        private void GenerateMusicalNoteArray(float stratingFrequency)
        {
            if (MusicalNotes == null)
            {
                MusicalNotes = new List<MusicalNote>();
            }
            else MusicalNotes.Clear();

            float temp = stratingFrequency;
            int rank = 0;

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < noteNameContent.Length; j++)
                {
                    if (noteNameContent[j].Length == 1)
                    {
                        AddToMusicalNotes(
                            new MusicalNote($"{noteNameContent[j]}{i}", temp, rank++));
                        temp = temp * 1.05946f;
                    }
                    else
                    {
                        if (noteNameContent[j][1] == '#')
                        {
                            AddToMusicalNotes(
                                new MusicalNote($"{noteNameContent[j]}{i}", temp, rank));
                            AddToMusicalNotes(
                                new MusicalNote($"{noteNameContent[j + 1]}{i}", temp, rank++));
                            temp = temp * 1.05946f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates the MusicalNote array from given note name and frequency.
        /// </summary>
        /// <throws>ArgumentException</throws>
        public MusicalNote[] GenerateMusicalNoteArray(string note, float frequency)
        {
            if (MusicalNotes == null)
            {
                GenerateMusicalNoteArray(440);
            }
            try
            {
                var s = MusicalNotes.Find(x => x.Name == note).Frequency;
                var b = MusicalNotes[0].Frequency;
                var d = s / b;
                frequency /= d;
                if (!MusicalNote.IsValidFrequency(frequency)) 
                    throw new ArgumentException();
                GenerateMusicalNoteArray(frequency);
            }
            catch (ArgumentException e)
            {
                MusicalNotes.Clear();
                
                throw new ArgumentException(e.Message);
            }
            finally { }

            return MusicalNotes.ToArray();
        }

        private void AddToMusicalNotes(MusicalNote note)
        {
            if (MusicalNotes == null)
            {
                MusicalNotes = new List<MusicalNote>();
            }
            MusicalNotes.Add(note);
        }

        /// <summary>
        /// Searching for file with generated array
        /// </summary>
        /// <returns>true if file is found</returns>
        private bool FindFileWithMusicalNoteArray()
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool SaveMusicalNoteArrayToFile()
        {
            throw new NotImplementedException();
        }

        private bool FindFileWithSettings()
        {
            throw new NotImplementedException();
        }

        private void UseDefaultSettings()
        {
            throw new NotImplementedException();
        }

        public bool SaveUserSettings(Settings setting)
        {
            return false;
        }

        public bool ChangeFrequencies(MusicalNote note)
        {
            throw new NotImplementedException();
        }
    }
}