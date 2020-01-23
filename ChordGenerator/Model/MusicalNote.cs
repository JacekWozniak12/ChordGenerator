using ChordGenerator.Model;
using System;

namespace ChordGenerator
{
    /// <summary>
    /// Handles musical notes from C0 to C9.
    /// Takes any note in syntaxes: X+D or X+M+D where
    /// X is the letter from A to G;
    /// M is one of two chars: #, b;
    /// D is single digit number 0 to 9;
    /// </summary>

    [Serializable]
    public class MusicalNote : Note
    {
        // In hertz
        private const double MINIMAL_FREQUENCY = 16;

        private const double MAXIMAL_FREQUENCY = 20000;
        private double _frequency;

        /// <summary>
        /// Can be changed by settings;
        /// </summary>
        public double Frequency
        {
            get => _frequency;
            set
            {
                if (IsValidFrequency(value))
                {
                    _frequency = value;
                }
                else throw new ArgumentException();
            }
        }

        /// <summary>
        /// Used to sygnalize if note is higher, lower or at same frequency as other in note array.
        /// </summary>
        public int Rank { get; set; }

        public MusicalNote(string name, double frequency, int rank)
        {
            Name = name;
            Frequency = frequency;
            Rank = rank;
        }

        public MusicalNote(string input, int rank)
        {
            var i = input.Split(':');
            Name = i[0];
            Frequency = Int32.Parse(i[1]);
            Rank = rank;
        }

        public MusicalNote(MusicalNote note)
        {
            Name = note.Name;
            Frequency = note.Frequency;
            Rank = note.Rank;
        }

        public MusicalNote()
        {
        }

        /// <summary>
        /// Returns string as "Name: Frequency", for example: "A4: 440"
        /// </summary>
        public override string ToString()
        {
            return $"{Name}: {Frequency}";
        }

        /// <summary>
        /// Checks if frequency is hearable.
        /// </summary>
        public static bool IsValidFrequency(double frequency)
        {
            if (frequency < MINIMAL_FREQUENCY || frequency > MAXIMAL_FREQUENCY)
            {
                return false;
            }
            else return true;
        }
    }
}