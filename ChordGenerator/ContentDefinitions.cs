using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes
    /// </summary>
    public class Settings
    {
        public enum Type
        {
            Pitch,
            Volume,
            // SynthType,
            Delay,
            DefaultTypeOfPlay
        }

        /// <summary>
        /// Opens file stream and search for config file
        /// </summary>
        public Settings()
        {
            
        }

        public Settings
            (
            float volume,
            int pitch,
            Chord.PlayType defaultPlayType,
            float defaultTimeToPlaySingleNote,
            float defaultTimeToPlayChord
            )
        {
            this.volume = volume;
            this.pitch = pitch;
            this.defaultPlayType = defaultPlayType;
            this.defaultTimeToPlaySingleNote = defaultTimeToPlaySingleNote;
            this.defaultTimeToPlayChord = defaultTimeToPlayChord;
        }

        public float volume = 0.5f;
        public int pitch = 0;

        public Chord.PlayType 
            defaultPlayType = Chord.PlayType.AllATSameTime;
        public float 
            defaultTimeToPlaySingleNote = 0.33f;
        public float
            defaultTimeToPlayChord = 2f;


        // read settings from stored file
    }

    /// <summary>
    /// Chord created by user;
    /// </summary>
    public struct Chord
    {
        private const int MaximalNotesPerChord = 30;

        private MusicalNote[] musicalNotes;

        public Chord(MusicalNote[] notes)
        {
            // TODO
            if (notes.Length > MaximalNotesPerChord) 
                throw new ArgumentException
                    ($"To many notes {notes.Length}\nReduce amount by {notes.Length - MaximalNotesPerChord}");


            musicalNotes =                  notes;
            defaultPlayType =               PlayType.AllATSameTime;
            defaultTimeToPlaySingleNote =   0.33f;
        }

        public enum PlayType
        {
            /// <summary>
            /// Chord notes played one after another
            /// </summary>
            Single,
            /// <summary>
            /// All chord notes played at the same time
            /// </summary>
            AllATSameTime
        }

        /// <summary>
        /// How will chord be played.
        /// </summary>
        private static PlayType defaultPlayType;
        /// <summary>
        /// In seconds
        /// </summary>
        private static float defaultTimeToPlaySingleNote;

        /// <summary>
        /// Plays the chord with defaut settings
        /// </summary>
        /// <param name="chord"></param>
        public static void Play(Chord chord)
        {
            Play(chord, defaultPlayType, defaultTimeToPlaySingleNote);
        }

        /// <summary>
        /// Plays the Chord
        /// </summary>
        /// <param name="chord"></param>
        /// <param name="playType"></param>
        /// <param name="timeToPlaySingleNote"></param>
        public static void Play(Chord chord, PlayType playType, float timeToPlaySingleNote)
        {
            switch (playType)
            {
                
            }
        }
    }

    /// <summary>
    /// Handles musical notes from C0 to C9. 
    /// Takes any note in syntaxes: X+D or X+M+D where 
    /// X is the letter from A to G;
    /// M is one of two chars: #, b;
    /// D is single digit number 0 to 9;
    /// </summary>
    public struct MusicalNote
    {
        /// <summary>
        /// Can't be changed by settings, initialized once;
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Can be changed by settings;
        /// </summary>
        private float frequency;

        public MusicalNote(string name, float frequency, int Rank)
        {
            this.name = name;
            this.frequency = frequency;
        }

        public override string ToString()
        {
            return $"{name}: {frequency}";
        }

        public bool ChangeFrequency(float Frequency)
        {
            throw new NotImplementedException();
            try
            {
                this.frequency = Frequency;
            }
            catch
            {

                return false;
            }

            return true;
        }

        /// <summary>
        /// First part of note. Example: [C]#3
        /// </summary>
        private readonly static char[] NameChars =
            {'A', 'B', 'C', 'D', 'E', 'F', 'G'};

        /// <summary>
        /// Second part of note, not obligatory. Example: C[#]3
        /// </summary>
        private readonly static char[] SpecialChars =
            { '#', 'b' };

        /// <summary>
        /// Check if given note name is valid
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValidName(string name)
        {
            if (name.Length > 3 || name.Length < 2) return false;

            var temp = name[0];
            int NotDigitChars = 0;

            foreach (var s in NameChars)
            {
                if (temp == s)
                {
                    NotDigitChars++;
                    break;
                }
            }

            if (NotDigitChars == 0) return false;

            temp = name[1];

            foreach (var s in SpecialChars)
            {
                if (temp == s)
                {
                    NotDigitChars++;
                    break;
                }
            }

            if (NotDigitChars > 1)
            {
                if (name.Length == 2) return false;
                else
                {
                    return Char.IsDigit(name[2]);
                }
            }
            else if (name.Length > 2) return false;

            return Char.IsDigit(name[1]);
        }

        // In hertz
        private const int MinimalFreq = 16;
        private const int MaximalFreq = 20000;

        /// <summary>
        /// Checks if frequency is hearable.
        /// </summary>
        /// <param name="Frequency"></param>
        /// <returns></returns>
        public static bool IsValidFrequency(float frequency)
        {
            if (frequency < MinimalFreq || frequency > MaximalFreq) 
                return false;
            else return true;
        }
    }
}
