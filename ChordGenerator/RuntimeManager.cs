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
        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array.
        /// </summary>
        public void GenerateMusicalNoteArray()
        {

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
