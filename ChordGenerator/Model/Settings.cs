using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave.SampleProviders;

namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes
    /// </summary>
    public class Settings
    {
        public enum Type
        {
            Pitch,
            Volume,
            SynthType,
            Delay,
            DefaultTypeOfPlay
        }

        public void Change(Type type)
        {

        }

        /// <summary>
        /// Opens file stream and search for config file
        /// </summary>
        public Settings(string fileAdress)
        {

        }

        public Settings
            (
            float volume,
            int pitch,
            Chord.PlayType defaultPlayType,
            float defaultTimeToPlaySingleNote,
            float defaultTimeToPlayChord
            )
        {
            this.volume = volume;
            this.pitch = pitch;
            this.defaultPlayType = defaultPlayType;
            this.defaultTimeToPlaySingleNote = defaultTimeToPlaySingleNote;
            this.defaultTimeToPlayChord = defaultTimeToPlayChord;
        }

        public float volume { get; private set; } = 0.5f;
        public int pitch { get; private set; }  = 0;
        public Chord.PlayType defaultPlayType { get; private set; } = 
            Chord.PlayType.AllATSameTime;
        public float defaultTimeToPlaySingleNote { get; private set; } = 
            0.33f;
        public float defaultTimeToPlayChord { get; private set; } = 
            2f;

        // read settings from stored file
    }
}
