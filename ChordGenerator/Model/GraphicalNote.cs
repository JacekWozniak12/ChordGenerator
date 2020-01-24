using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChordGenerator.Model
{
    public class GraphicalNote : Note, INotifyPropertyChanged
    {
        private Brush _color = Brushes.Transparent;

        public Brush Color { 
            get => _color; 
            set => _color = value; 
        }

        public GraphicalNote(MusicalNote note)
        {
            base.Name = note.Name;
        }

        public GraphicalNote() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
