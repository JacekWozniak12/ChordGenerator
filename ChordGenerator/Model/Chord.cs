using ChordGenerator.Model;
using System;
using System.ComponentModel;

namespace ChordGenerator
{
    /// <summary>
    /// Chord created by user;
    /// </summary>
    [Serializable]
    public struct Chord : INotifyPropertyChanged
    {
        private const int MAXIMAL_NOTES_PER_CHORD = 30;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public Note[] MusicalNotes { get; set; }

        public Chord(Note[] notes)
        {
            if (notes.Length > MAXIMAL_NOTES_PER_CHORD)
                throw new ArgumentException
                    ($"To many notes {notes.Length}\nReduce amount by {notes.Length - MAXIMAL_NOTES_PER_CHORD}");

            MusicalNotes = notes;
            Name = $"";
            foreach (var note in notes)
            {
                Name += $"{note.Name} ";
            }
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