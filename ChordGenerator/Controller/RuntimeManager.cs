using ChordGenerator.Controller;
using ChordGenerator.Model;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {
        public static RuntimeManager Instance { get; private set; }
        public Settings RuntimeSettings;
        public List<Chord> ChordsPlayed;        
        
        IOHandler ioHandler = new IOHandler();
        NAudioCommunication nAudioCommunication;

        /// <summary>
        /// Handles startup
        /// </summary>
        public RuntimeManager()
        {
            Instance = this;
            nAudioCommunication = new NAudioCommunication();
            RuntimeSettings = ioHandler.HandleReadingSettings(obj: new Settings());
            ChordsPlayed = ioHandler.HandleReadingChords(obj: new List<Chord>());
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
                if (ChordsPlayed.Count == 0) return;
                Chord chord = ChordsPlayed[ChordsPlayed.Count - 1];
                List<MusicalNote> chordWithFrequencies = new List<MusicalNote>();
                foreach(var n in chord.MusicalNotes)
                {
                    chordWithFrequencies.Add(RuntimeSettings.MusicalNotes.Find(x => x.Name == n.Name));
                }

                if (AllAtOnce)
                {
                    nAudioCommunication.
                            PlaySound(
                            chordWithFrequencies.ToArray(),
                            RuntimeSettings.Volume,
                            RuntimeSettings.Duration,
                            SignalGeneratorType.Sin //todo przenieść to
                        );
                }
                else
                {
                    foreach (var note in chordWithFrequencies)
                    {
                        nAudioCommunication.
                            PlaySound(
                            note,
                            RuntimeSettings.Volume,
                            RuntimeSettings.Duration
                            / chord.MusicalNotes.Length,
                            SignalGeneratorType.Sin //todo przenieść to
                        );

                        await Task.Delay((int)RuntimeSettings.Duration
                            / chord.MusicalNotes.Length * 1000);
                    }
                }

            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public void ChangeNoteArray(string note, float frequency)
        {
            nAudioCommunication.AudioOut.Stop();
            RuntimeSettings.GenerateMusicalNoteArray(note, frequency);
        }

        public void Close()
        {
            nAudioCommunication.Dispose();
            ioHandler.HandleSavingSettings(obj: RuntimeSettings);
            ioHandler.HandleSavingChords(obj: ChordsPlayed);
        }
    }
}