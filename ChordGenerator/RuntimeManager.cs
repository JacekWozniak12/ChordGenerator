using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordGenerator
{
    /// <summary>
    /// may change name later
    /// </summary>
    public class RuntimeManager
    {

        List<MusicalNote> musicalNotes;

        string[] noteNameContent =
        {
            "C", "C#", "Db", "D", "D#", 
            "Eb", "E", "F", "F#", "Gb",
            "G", "G#", "Ab", "A", "A#",
            "Bb", "B"
        };

        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array.
        /// </summary>
        public void GenerateMusicalNoteArray(float defaultFrequency)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < noteNameContent.Length; j++)
                {
                    if(noteNameContent[j].Length == 1)
                    {
                            AddToMusicalNotes(new MusicalNote($"{noteNameContent[j]}{i}", 55));
                    }
                    else
                    {
                        if (noteNameContent[j][1] == '#')
                        {
                            AddToMusicalNotes(new MusicalNote($"{noteNameContent[j]}{i}",   55));
                            AddToMusicalNotes(new MusicalNote($"{noteNameContent[j+1]}{i}", 55));
                        }
                    }
                }
            }
        }

        // https://pages.mtu.edu/~suits/scales.html
        // https://pages.mtu.edu/~suits/chords.html

        private void AddToMusicalNotes(MusicalNote note)
        {
            if(musicalNotes == null)
            {
                musicalNotes = new List<MusicalNote>();
            }
            musicalNotes.Add(note);
        }

        /// <summary>
        /// Generates
        /// </summary>
        /// <returns>true if file is found</returns>
        public bool FindFileWithMusicalNoteArray()
        {
            // reads input output
            throw new NotImplementedException();

            try
            {

                return true;
            }
            catch (IOException e)
            {
               
            }
            return false;
        }

        public bool SaveMusicalNoteArrayToFile()
        {
            throw new NotImplementedException();           
        }

        public bool FindFileWithSettings()
        {
            throw new NotImplementedException();
        }
        
        public void UseDefaultSettings()
        {
            throw new NotImplementedException();
        }

        /*public bool SaveUserSettings(Setting setting)
        {

        }*/

        public bool ChangeFrequencies(MusicalNote note)
        {
            throw new NotImplementedException();
        }
    }
    
}
