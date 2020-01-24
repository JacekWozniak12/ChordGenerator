using ChordGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes, like note dictionary or volume
    /// </summary>
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        public const float  MINIMAL_DURATION = 00.5f;
        public const float  MAXIMAL_DURATION = 15.0f;
        public const double BASE_STARTING_FREQUENCY = 16.355;
        public const int    MAXIMAL_VOLUME = 1;
        public const float  MINIMAL_VOLUME = 0.01f;
        public const double MINIMAL_A4FREQ = 432d;
        public const double MAXIMAL_A4FREQ = 450d;

        public Guitar Guitar { get; set; }
        public float Volume 
        {
            get => _volume;
            set 
            {
                _volume = Clamp(value, MINIMAL_VOLUME, MAXIMAL_VOLUME);
            }
        }
       
        public float Duration 
        { 
            get => _duration;
            set 
            {
                _duration = Clamp(value, MINIMAL_DURATION, MAXIMAL_DURATION);
            }    
        }

        private double _a4Frequency = 440f;
        private float _volume = 0.5f;

        public double A4Frequency 
        { 
            get => _a4Frequency; 
            set
            {
               _a4Frequency = Clamp(value, MINIMAL_A4FREQ, MAXIMAL_A4FREQ);
               GenerateMusicalNoteArray("A4", _a4Frequency);
            }
        }

        public List<MusicalNote> MusicalNotes;

        private string[] noteNameContent =
        {
            "C", "C#", "Db", "D", "D#",
            "Eb", "E", "F", "F#", "Gb",
            "G", "G#", "Ab", "A", "A#",
            "Bb", "B"
        };
        private float _duration = 2f;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array. Uses the frequency for starting point
        /// </summary>
        private void GenerateMusicalNoteArray(double stratingFrequency)
        {
            if (MusicalNotes == null)
            {
                MusicalNotes = new List<MusicalNote>();
            }

            MusicalNotes.Clear();

            double temp = stratingFrequency;
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

        public MusicalNote[] GenerateNoteArrayFromAnotherNoteArray(MusicalNote note, MusicalNote[] array, int amount)
        {
            List<MusicalNote> musicalNotes = new List<MusicalNote>();

            var arrayOfNotes = new List<MusicalNote>(array);
            var a = arrayOfNotes.Find(x => x.Name == note.Name).Rank;

            for (int i = 0; i < amount; i++)
            {
                musicalNotes.Add(arrayOfNotes.Find(x => x.Rank == a));
                a++;
            }
            return musicalNotes.ToArray();
        }

        /// <summary>
        /// Generates the MusicalNote array from given note name and frequency.
        /// </summary>
        /// <throws>ArgumentException</throws>
        public MusicalNote[] GenerateMusicalNoteArray(string note, double frequency)
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
            finally {
                GenerateMusicalNoteArray(BASE_STARTING_FREQUENCY);
            }
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
        /// Opens file stream and search for config file
        /// </summary>
        public Settings(string fileAdress)
        {
        }

        public Settings
            (
            float volume = 0.5f,
            float defaultTimeToPlaySingleNote = 0.33f,
            float defaultTimeToPlayChord = 2f
            )
        {
            this.Volume = volume;
            this.Duration = defaultTimeToPlaySingleNote;
            GenerateMusicalNoteArray(BASE_STARTING_FREQUENCY);
        }

        public Settings(Settings settings)
        {
            Volume = settings.Volume;
            Duration = settings.Duration;
            A4Frequency = 440;
        }

        public Settings()
        {
            GenerateMusicalNoteArray(BASE_STARTING_FREQUENCY);
        }

        private float Clamp(float value, float min, float max)
        {
            if (value > max)
                return max;
            else if
                (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        private double Clamp(double value, double min, double max)
        {
            if (value > max)
                return max;
            else if
                (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
    }
}