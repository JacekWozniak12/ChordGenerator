using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator.Model
{
    /// <summary>
    /// Object that can contain multiple strings from 1 up to 10.
    /// </summary>
    public struct Guitar
    {
        public const int MAXIMAL_STRING_AMOUNT = 10;
        public const int MINIMAL_STRING_AMOUNT = 1;

        public List<GuitarString> GuitarStrings { get; private set; }

        public Guitar(List<GuitarString> guitarStrings)
        {
            if (
                guitarStrings.Count > MAXIMAL_STRING_AMOUNT ||
                guitarStrings.Count < MINIMAL_STRING_AMOUNT
                ) 
                throw new ArgumentException();

            GuitarStrings = new List<GuitarString>(guitarStrings);
        }     

    }
}
