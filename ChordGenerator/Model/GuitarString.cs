using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChordGenerator.Model
{
    /// <summary>
    /// Object that has 24 notes.
    /// Uses const to prevent creating unrealistic string setups.
    /// </summary>
    [Serializable]
    public struct GuitarString : INotifyPropertyChanged
    {
        public const double MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 30.87f;
        public const double MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 440f;
        public const int STRINGS_NOTES = 24;

        private Settings settings;

        public event PropertyChangedEventHandler PropertyChanged;

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
            notesOnString = new List<MusicalNote>();
            Creator(musicalNote);
        }

        private void Creator(MusicalNote musicalNote)
        {
            if (musicalNote.Frequency < MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE)
            {
                notesOnString.AddRange(
                    settings.
                    GenerateNoteArrayFromAnotherNoteArray
                    (settings.MusicalNotes.Find
                    (x => x.Frequency >= MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE),
                    settings.MusicalNotes.ToArray(), STRINGS_NOTES)
                );
            }
            else
            if (musicalNote.Frequency > MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE)
            {
                notesOnString.AddRange(
                settings.
                    GenerateNoteArrayFromAnotherNoteArray
                    (settings.MusicalNotes.FindLast
                    (x => x.Frequency <= MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE),
                    settings.MusicalNotes.ToArray(), STRINGS_NOTES)
                );
            }
            else
            {
                notesOnString.AddRange(
                settings.GenerateNoteArrayFromAnotherNoteArray
                    (musicalNote, settings.MusicalNotes.ToArray(), STRINGS_NOTES)
                    );
            }
        }
    }
}