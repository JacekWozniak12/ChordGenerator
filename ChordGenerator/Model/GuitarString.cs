using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

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
        public const int STRINGS_NOTES = 25;
        private Settings settings;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        /// <summary>
        /// From the lowest to the highest note;
        /// </summary>
        public ObservableCollection<MusicalNote> NotesOnString
        {
            get => _notesOnString;
            set
            {
                OnPropertyRaised("NotesOnString");
                _notesOnString = value;
            }
        }

        private ObservableCollection<MusicalNote> _notesOnString;

        /// <summary>
        /// Creates single string from given note.
        /// If note is to low/high for string to handle, then string
        /// creates from minimal / maximal const
        /// </summary>
        public GuitarString(MusicalNote note)
        {
            NotesOnString = new ObservableCollection<MusicalNote>();
            NotesOnString = Creator(note);
            SetColors();
        }

        private ObservableCollection<GraphicalNote> graphicalNotes = new ObservableCollection<GraphicalNote>();

        public ObservableCollection<GraphicalNote> GraphicalNotes
        {
            get => graphicalNotes;
            set
            {
                OnPropertyRaised("GraphicalNotes");
                graphicalNotes = value;
            }
        }

        public void SetColors()
        {
            if (RuntimeManager.Instance.SelectedChord == null)
            {
                for (int i = 0; i < STRINGS_NOTES; i++)
                {
                    GraphicalNotes.Add(new GraphicalNote(NotesOnString[i]));
                }
            }
            else
            {
                for (int i = 0; i < STRINGS_NOTES; i++)
                {
                    var t = 0;

                    foreach (var test in RuntimeManager.Instance.SelectedChord.MusicalNotes)
                    {
                        if (NotesOnString[i].Name == test.Name)
                        {
                            t++;
                            var x = new GraphicalNote(NotesOnString[i]);
                            x.Color = Brushes.Red;
                            GraphicalNotes.Add(x);
                        }
                    }
                    if (t == 0)
                    {
                        GraphicalNotes.Add(new GraphicalNote(NotesOnString[i]));
                    }
                }
            }
        }

        public void SetSettings(Settings settings)
        {
            this.settings = settings;
        }

        private ObservableCollection<MusicalNote> Creator(MusicalNote note)
        {
            List<MusicalNote> notes = new List<MusicalNote>();
            MusicalNote musicalNote = note;

            if (settings == null)
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
            return new ObservableCollection<MusicalNote>(notes);
        }
    }
}