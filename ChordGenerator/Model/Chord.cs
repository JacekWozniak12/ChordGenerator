using ChordGenerator.Model;
using System;

namespace ChordGenerator
{
    /// <summary>
    /// Chord created by user;
    /// </summary>
    [Serializable]
    public struct Chord
    {
        private const int MAXIMAL_NOTES_PER_CHORD = 30;

        public string Name;
        
        public Note[] MusicalNotes { get; set; }

        public Chord(Note[] notes)
        {
            if (notes.Length > MAXIMAL_NOTES_PER_CHORD)
                throw new ArgumentException
                    ($"To many notes {notes.Length}\nReduce amount by {notes.Length - MAXIMAL_NOTES_PER_CHORD}");

            MusicalNotes = notes;
            Name = "Chord";
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