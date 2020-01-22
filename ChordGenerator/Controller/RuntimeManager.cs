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
        public Settings runtimeSettings;
        public List<Chord> ChordsPlayed = new List<Chord>();

        /// <summary>
        /// Handles startup
        /// </summary>
        public RuntimeManager()
        {
            Instance = this;
            runtimeSettings = new Settings();
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