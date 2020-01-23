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
    public struct MusicalNote
    {
        // In hertz
        private const double MINIMAL_FREQUENCY = 16;
        private const double MAXIMAL_FREQUENCY = 20000;

        /// <summary>
        /// First part of note. Example: [C]#3
        /// </summary>
        private readonly static char[] NAME_CHARS =
            {'A', 'B', 'C', 'D', 'E', 'F', 'G'};

        /// <summary>
        /// Second part of note, not obligatory. Example: C[#]3
        /// </summary>
        private readonly static char[] SPECIAL_CHARS =
            { '#', 'b' };

        private string _name;
        private double _frequency;

        /// <summary>
        /// Can be changed by settings;
        /// </summary>
        public double Frequency
        {
            get => _frequency;
            private set
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
        public int Rank { get; private set; }

        /// <summary>
        /// Can't be changed by settings, initialized once;
        /// </summary>
        public string Name
        {
            get => _name;
            private set
            {
                if (IsValidName(value))
                {
                    _name = value;
                }
                else throw new ArgumentException();
            }
        }

        public MusicalNote(string name, double frequency, int rank)
        {
            this = new MusicalNote();
            Name = name;
            Frequency = frequency;
            Rank = rank;
        }

        public MusicalNote(string input, int rank)
        {
            this = new MusicalNote();
            var i = input.Split(':');
            Name = i[0];
            Frequency = Int32.Parse(i[1]);
            Rank = rank;
        }

        public MusicalNote(MusicalNote note)
        {
            this = new MusicalNote();
            Name = note.Name;
            Frequency = note.Frequency;
            Rank = note.Rank;
        }

        /// <summary>
        /// Returns string as "Name: Frequency", for example: "A4: 440"
        /// </summary>
        public override string ToString()
        {
            return $"{Name}: {Frequency}";
        }

        /// <summary>
        /// Check if given note name is valid
        /// </summary>
        public static bool IsValidName(string name)
        {
            if (name.Length > 3 || name.Length < 2)
            {
                return false;
            }

            var temp = name[0];
            int NotDigitChars = 0;

            foreach (var s in NAME_CHARS)
            {
                if (temp == s)
                {
                    NotDigitChars++;
                    break;
                }
            }

            if (NotDigitChars == 0) return false;

            temp = name[1];

            foreach (var s in SPECIAL_CHARS)
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