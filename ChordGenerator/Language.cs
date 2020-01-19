using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// Handles language files
    /// </summary>
    public class Language
    {
        public readonly string Button_Generate = "Generate!";
        public readonly string Button_LearnChords = "Learn chords";

        public readonly string Info_Title = "Chord Generator";
        public readonly string Info_EnterChord = "Enter chord";

        public readonly string Error_InvalidFreq = "Invalid note frequency";
        public readonly string Error_InvalidNoteName = "Invalid note name";
        public readonly string Error_InvalidSetting = "Invalid setting name or paramter";
        public readonly string Error_InvalidSyntax = "Invalid chord syntax";

        public Language(params string[] s)
        {
            Button_Generate = s[0];
            Button_LearnChords = s[1];
            
            Info_Title = s[2];
            Info_EnterChord = s[3];


        }

        public Language() { }
    }
}
