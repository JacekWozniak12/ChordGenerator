using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChordGenerator.Model
{
    /// <summary>
    /// Object that can contain multiple strings from 1 up to 10.
    /// </summary>
    [Serializable]
    public class Guitar : INotifyPropertyChanged
    {
        public const int MAXIMAL_STRING_AMOUNT = 10;
        public const int MINIMAL_STRING_AMOUNT = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public ObservableCollection<GuitarString> GuitarStrings
        {
            get;
            set;
        }

        public Guitar(List<GuitarString> guitarStrings)
        {
            if (
                guitarStrings.Count > MAXIMAL_STRING_AMOUNT ||
                guitarStrings.Count < MINIMAL_STRING_AMOUNT
                )
                throw new ArgumentException();

            GuitarStrings = new ObservableCollection<GuitarString>(guitarStrings);
        }

        private readonly MusicalNote[] DEFAULT_OPEN_STRINGS =
            {
                new MusicalNote("E2", 82.41, 0),
                new MusicalNote("A2", 110, 0),
                new MusicalNote("D3", 146.83, 0),
                new MusicalNote("G3", 196.00, 0),
                new MusicalNote("B3", 246.94, 0),
                new MusicalNote("E4", 329.63, 0)
            };

        public Guitar()
        {
            OnPropertyRaised("Guitar");
            GuitarStrings = new ObservableCollection<GuitarString>();
            for (int i = 0; i < 6; i++)
            {
                var s = new GuitarString((DEFAULT_OPEN_STRINGS[i]));
                GuitarStrings.Add(s);
            }
        }
    }
}