using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator.Model
{
    struct GuitarString
    {
        public const double MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 30.87f;
        public const double MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 440f;
        public const int STRINGS_NOTES = 24;

        Settings settings;
      
        /// <summary>
        /// From the lowest to the highest note;
        /// </summary>
        public List<MusicalNote> notesOnString { get; private set; }

        /// <summary>
        /// Creates single string from given note. 
        /// If note is to low/high for string to handle, then string 
        /// creates from minimal / maximal const
        /// </summary>
        public GuitarString(MusicalNote musicalNote, Settings settings)
        {
            this.settings = settings;
            Creator(musicalNote);
        }

        private void Creator(MusicalNote musicalNote)
        {
            if (musicalNote.Frequency < MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE)
            {
                settings.
                    GenerateNoteArrayFromAnotherNoteArray
                    (settings.MusicalNotes.Find
                    (x => x.Frequency >= MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE),
                    settings.MusicalNotes.ToArray(), STRINGS_NOTES);
            }
            else
            if (musicalNote.Frequency > MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE)
            {
                settings.
                    GenerateNoteArrayFromAnotherNoteArray
                    (settings.MusicalNotes.FindLast
                    (x => x.Frequency <= MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE), 
                    settings.MusicalNotes.ToArray(), STRINGS_NOTES);
            }
            else
            {
                settings.GenerateNoteArrayFromAnotherNoteArray
                    (musicalNote, settings.MusicalNotes.ToArray(), STRINGS_NOTES);
            }
        }
    }
}
