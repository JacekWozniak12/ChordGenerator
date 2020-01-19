using System;

namespace ChordGenerator
{
    /// <summary>
    /// Chord created by user;
    /// </summary>
    public struct Chord
    {
        private const int MaximalNotesPerChord = 30;

        public MusicalNote[] musicalNotes { get; private set; }

        public Chord(MusicalNote[] notes)
        {
            // TODO
            if (notes.Length > MaximalNotesPerChord) 
                throw new ArgumentException
                    ($"To many notes {notes.Length}\nReduce amount by {notes.Length - MaximalNotesPerChord}");


            musicalNotes =                  notes;
            defaultPlayType =               PlayType.AllATSameTime;
            defaultTimeToPlaySingleNote =   0.33f;
        }

        public enum PlayType
        {
            /// <summary>
            /// Chord notes played one after another
            /// </summary>
            Single,
            /// <summary>
            /// All chord notes played at the same time
            /// </summary>
            AllATSameTime
        }

        /// <summary>
        /// How will chord be played.
        /// </summary>
        private static PlayType defaultPlayType;
        /// <summary>
        /// In seconds
        /// </summary>
        private static float defaultTimeToPlaySingleNote;
    }
}
