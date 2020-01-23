using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {
        private NAudioCommunication nAudioCommunication;
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
            nAudioCommunication = new NAudioCommunication();
        }

        /// <summary>
        /// Handles playing sound in view
        /// </summary>
        /// <param name="chord"></param>
        public void AddSound(Chord chord)
        {
            try
            {
                ChordsPlayed.Add(chord);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public async void PlaySound()
        {
            bool AllAtOnce = false;

            try
            {
                var chord = ChordsPlayed[ChordsPlayed.Count - 1];
                if (AllAtOnce)
                { 
                    nAudioCommunication.
                            PlaySound(
                            chord,
                            runtimeSettings.Volume,
                            runtimeSettings.Duration,
                            SignalGeneratorType.Sin //todo przenieść to
                        );
                }
                else
                {
                    foreach(var note in chord.musicalNotes)
                    {
                        nAudioCommunication.
                            PlaySound(
                            note,
                            runtimeSettings.Volume,
                            runtimeSettings.Duration 
                            / chord.musicalNotes.Length,
                            SignalGeneratorType.Sin //todo przenieść to
                        );

                        await Task.Delay((int)runtimeSettings.Duration 
                            / chord.musicalNotes.Length * 1000);
                    }
                }

            }
            catch (ArgumentException e)
            {
                
            }
        }

        public void ChangeNoteArray(string note, float frequency)
        {
            nAudioCommunication.AudioOut.Stop();
            runtimeSettings.GenerateMusicalNoteArray(note, frequency);
        }

        public void Dispose()
        {
            nAudioCommunication.Dispose();
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