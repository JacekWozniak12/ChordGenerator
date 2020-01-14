﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator
{
    // {x}(Frequency} 
    // sets frequency of note x;

    // [setting](value)
    // sets value of known setting

    // (x)
    // note x

    // (x + 7)
    // note being 7 semitones further than x

    // x ^ (x + 7)
    // note x and note being 7 semitones further than x

    // x ^ y
    // note x and note y

    // x + 5 ^ y - 2
    // note being 5 semitones further than x and 
    // note being 2 semitones behing y

    public class SyntaxReader
    {
        /// <summary>
        /// Reads the Chord content
        /// </summary>
        public void ReadChord()
        {
            try
            {

            }
            catch
            {

            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to read the settings
        /// </summary>
        public void ReadSetting()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to read the Note, next tries to change frequency of it and
        /// every single note in base.
        /// </summary>
        public void ReadNoteToChange(string Note, float frequency)
        {
            throw new NotImplementedException();
        }


    }

    
}
