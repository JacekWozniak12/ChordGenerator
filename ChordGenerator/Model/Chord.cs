using System;

namespace ChordGenerator
{
    /// <summary>
    /// Chord created by user;
    /// </summary>
    [Serializable]
    public struct Chord
    {
        public string Name;

        private const int MaximalNotesPerChord = 30;

        public MusicalNote[] MusicalNotes { get; set; }

        public Chord(MusicalNote[] notes)
        {
            if (notes.Length > MaximalNotesPerChord)
                throw new ArgumentException
                    ($"To many notes {notes.Length}\nReduce amount by {notes.Length - MaximalNotesPerChord}");

            MusicalNotes = notes;
            Name = "chord";
        }

        public new string ToString()
        {
            string result = "";

            foreach (var item in MusicalNotes)
            {
                result += $"{item.Name} ^ ";
            }

            return result.Substring(0, result.Length - 1).Trim();
        }
    }
}