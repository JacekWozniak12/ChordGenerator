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
    public class GuitarString : INotifyPropertyChanged
    {
        public const double MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 30.87f;
        public const double MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE = 440f;
        public const int STRINGS_NOTES = 24;
        private Settings settings;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// From the lowest to the highest note;
        /// </summary>
        public List<MusicalNote> NotesOnString
        {
            get => _notesOnString;
            set
            {
                _notesOnString = value;
            } 
        }
        private List<MusicalNote>  _notesOnString;

        /// <summary>
        /// Creates single string from given note.
        /// If note is to low/high for string to handle, then string
        /// creates from minimal / maximal const
        /// </summary>
        public GuitarString(MusicalNote note)
        {
            NotesOnString = new List<MusicalNote>();
            NotesOnString = Creator(note);
        }

        public void SetSettings(Settings settings)
        {
            this.settings = settings;
        }

        private List<MusicalNote> Creator(MusicalNote note)
        {
            List<MusicalNote> notes = new List<MusicalNote>();
            MusicalNote musicalNote = note;

            if(settings == null)
            {
                settings = new Settings();
            }
            musicalNote = settings.MusicalNotes.Find(x => x.Name == note.Name);
            if (musicalNote.Frequency < MINIMAL_OPENSTRING_FREQUENCY_POSSIBLE)
                {
                    notes.AddRange(
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
                    notes.AddRange(
                    settings.
                        GenerateNoteArrayFromAnotherNoteArray
                        (settings.MusicalNotes.FindLast
                        (x => x.Frequency <= MAXIMAL_OPENSTRING_FREQUENCY_POSSIBLE),
                        settings.MusicalNotes.ToArray(), STRINGS_NOTES)
                    );
                }
                else
                {
                    notes.AddRange(
                    settings.GenerateNoteArrayFromAnotherNoteArray
                        (musicalNote, settings.MusicalNotes.ToArray(), STRINGS_NOTES)
                        );
                }
            settings = null;
            return notes;
        }
    }
}