namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes
    /// </summary>
    public class Settings
    {
        public enum Type
        {
            NoteTime,
            ChordTime,
            Volume,
            DefaultTypeOfPlay
        }

        public void Change(Type type, float value)
        {
            switch (type)
            {
                case Type.NoteTime:
                    this.defaultTimeToPlaySingleNote = value;
                    break;
                case Type.ChordTime:
                    this.defaultTimeToPlayChord = value;
                    break;
                case Type.Volume:
                    this.volume = value;
                    break;
                case Type.DefaultTypeOfPlay:  
                    this.defaultPlayType = (Chord.PlayType) (int) value;
                    break;
            }
        }

        /// <summary>
        /// Opens file stream and search for config file
        /// </summary>
        public Settings(string fileAdress)
        {

        }

        public Settings
            (
            float volume = 0.5f,
            Chord.PlayType defaultPlayType = Chord.PlayType.AllATSameTime,
            float defaultTimeToPlaySingleNote = 0.33f,
            float defaultTimeToPlayChord = 2f
            )
        {
            this.volume = volume;
            this.defaultPlayType = defaultPlayType;
            this.defaultTimeToPlaySingleNote = defaultTimeToPlaySingleNote;
            this.defaultTimeToPlayChord = defaultTimeToPlayChord;
        }

        public Settings(Settings settings)
        {
            volume = settings.volume;
            defaultPlayType = settings.defaultPlayType;
            defaultTimeToPlaySingleNote = settings.defaultTimeToPlaySingleNote;
            defaultTimeToPlayChord = settings.defaultTimeToPlayChord;
        }

        public float volume { get; private set; } = 0.5f;

        public Chord.PlayType defaultPlayType { get; private set; } =
            Chord.PlayType.AllATSameTime;

        public float defaultTimeToPlaySingleNote 
        { get; private set; } = 0.33f;

        public float defaultTimeToPlayChord 
        { get; private set; } = 2f;

        // read settings from stored file
    }
}