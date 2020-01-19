﻿using System;

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
        /// <summary>
        /// Can't be changed by settings, initialized once;
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Can be changed by settings;
        /// </summary>
        public float frequency { get; private set; }

        public MusicalNote(string name, float frequency)
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
