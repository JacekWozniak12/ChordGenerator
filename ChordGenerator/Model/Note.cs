using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator.Model
{
    /// <summary>
    /// Handles display part of app
    /// </summary>
    [Serializable]
    public class Note
    {
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
        /// <summary>
        /// Can't be changed by settings, initialized once;
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (IsValidName(value))
                {
                    _name = value;
                }
                else throw new ArgumentException();
            }
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

    }
}
