namespace ChordGenerator
{
    /// <summary>
    /// Handles language files
    /// </summary>
    public class Language
    {
        public readonly string Button_Generate = "Generate!";
        public readonly string Button_LearnChords = "Learn chords";

        public readonly string Info_Title = "Chord Generator";
        public readonly string Info_EnterChord = "Enter chord";

        public readonly string Error_InvalidFreq = "Invalid note frequency";
        public readonly string Error_InvalidNoteName = "Invalid note name";
        public readonly string Error_InvalidSetting = "Invalid setting name or parameter";
        public readonly string Error_InvalidSyntax = "Invalid chord syntax";

        public Language(params string[] s)
        {
        }

        public Language()
        {
        }
    }
}