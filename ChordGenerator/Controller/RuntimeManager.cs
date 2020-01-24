using ChordGenerator.Controller;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {
        private const int MAXIMAL_AMOUNT_OF_CHORDS = 30;

        public static RuntimeManager Instance { get; private set; }
        public Settings RuntimeSettings { get; set; }
        public ObservableCollection<Chord> ChordsPlayed { get; set; }

        private IOHandler ioHandler = new IOHandler();
        private NAudioCommunication nAudioCommunication;

        public Chord SelectedChord { get; set; }

        /// <summary>
        /// Handles startup
        /// </summary>
        public RuntimeManager()
        {
            Instance = this;
            
            nAudioCommunication = new NAudioCommunication();
            
            RuntimeSettings = ioHandler.HandleReadingSettings(obj: new Settings());
            
            ChordsPlayed = new ObservableCollection<Chord>(ioHandler.HandleReadingChords(obj: new List<Chord>()));
        }

        public bool AllAtOnce = false;

        /// <summary>
        /// Handles playing sound in view
        /// </summary>
        /// <param name="chord"></param>
        public void AddSound(Chord chord)
        {
            try
            {
                while (ChordsPlayed.Count > MAXIMAL_AMOUNT_OF_CHORDS)
                {
                    ChordsPlayed.Remove(ChordsPlayed[0]);
                }
                ChordsPlayed.Add(chord);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public async void PlaySound()
        {          
            try
            {
                Chord chord;
                chord = SelectedChord;
                if (ChordsPlayed.Count == 0) return;              
                foreach(var i in SelectedChord.MusicalNotes)
                {
                    if (!MusicalNote.IsValidName(i.Name))
                    {
                        chord = ChordsPlayed[ChordsPlayed.Count - 1];
                        break;
                    }
                }
                List<MusicalNote> chordWithFrequencies = new List<MusicalNote>();
                foreach (var n in chord.MusicalNotes)
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
            catch (Exception e)
            {
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
            ioHandler.HandleSavingChords(obj: new List<Chord>(ChordsPlayed));
        }
    }
}