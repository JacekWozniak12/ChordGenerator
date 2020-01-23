using ChordGenerator.Model;
using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes, like note dictionary or volume
    /// </summary>
    [Serializable]
    public class Settings
    {
        public const float MINIMAL_DURATION = 00.5f;
        public const float MAXIMAL_DURATION = 15.0f;

        public Guitar guitar { get; private set; }
        public float Volume { get; private set; } = 0.5f;
        public PlayType HowToPlay { get; private set; } = PlayType.AllAtTheSameTime;
        public float Duration { get; private set; } = 2f;
        public float TimeToPlayChord { get; private set; } = 2f;

        public List<MusicalNote> MusicalNotes;

        private string[] noteNameContent =
        {
            "C", "C#", "Db", "D", "D#",
            "Eb", "E", "F", "F#", "Gb",
            "G", "G#", "Ab", "A", "A#",
            "Bb", "B"
        };

        public enum Type
        {
            Duration,
            Volume,
            DefaultTypeOfPlay
        }

        public enum PlayType
        {
            AllAtTheSameTime,
            OneAfterAnother
        }

        public void Change(Type type, float value)
        {
            switch (type)
            {
                case Type.Duration:
                    this.Duration = value;
                    break;

                case Type.Volume:
                    this.Volume = value;
                    break;

                case Type.DefaultTypeOfPlay:
                    this.HowToPlay = (PlayType)(int)value;
                    break;
            }
        }

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
            else MusicalNotes.Clear();

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
        /// Opens file stream and search for config file
        /// </summary>
        public Settings(string fileAdress)
        {
        }

        public Settings
            (
            float volume = 0.5f,
            PlayType defaultPlayType = PlayType.AllAtTheSameTime,
            float defaultTimeToPlaySingleNote = 0.33f,
            float defaultTimeToPlayChord = 2f
            )
        {
            this.Volume = volume;
            this.HowToPlay = defaultPlayType;
            this.Duration = defaultTimeToPlaySingleNote;
            GenerateMusicalNoteArray(16.35f);
        }

        public Settings(Settings settings)
        {
            Volume = settings.Volume;
            HowToPlay = settings.HowToPlay;
            Duration = settings.Duration;
        }

        public Settings()
        {
            GenerateMusicalNoteArray(16.35f);
        }
    }
}