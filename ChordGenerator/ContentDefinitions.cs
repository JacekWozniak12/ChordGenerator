using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// Handles musical notes from Ab0 to F#9. 
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
        private readonly string Name;

        /// <summary>
        /// Can be changed by settings;
        /// </summary>
        private float Frequency;

        public MusicalNote(string Name, float Frequency, int Rank)
        {
            this.Name = Name;
            this.Frequency = Frequency;
        }

        public override string ToString()
        {
            return $"{Name}: {Frequency}";
        }

        public bool ChangeFrequency(float Frequency)
        {
            throw new NotImplementedException();
            try
            {
                this.Frequency = Frequency;
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
        /// <param name="Name"></param>
        /// <returns></returns>
        public static bool IsValidName(string Name)
        {
            if (Name.Length > 3 || Name.Length < 2) return false;

            var temp = Name[0];
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

            temp = Name[1];

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
                if (Name.Length == 2) return false;
                else
                {
                    return Char.IsDigit(Name[2]);
                }
            }
            else if (Name.Length > 2) return false;

            return Char.IsDigit(Name[1]);
        }

        // In hertz
        private const int MinimalFreq = 16;
        private const int MaximalFreq = 20000;

        public static bool IsValidFrequency(float Frequency)
        {
            if (Frequency < MinimalFreq || Frequency > MaximalFreq) 
                return false;
            else return true;
        }
    }
}
