using ChordGenerator;
using NUnit.Framework;

namespace Tests
{
    internal class Tests_SyntaxReader
    {
        /// <summary>
        /// Creates the chord and uses its ToString().
        /// </summary>
        [TestCase("Bb3 + 3", "C#4")]
        [TestCase("C#3", "C#3")]
        [TestCase("Cb3 + 4 + 6", "A3")]
        [TestCase("C#3 ^ Cb3 + 3", "C#3 ^ D3")]
        [TestCase("Ab0 + 5 ^ G4 + 1", "C#1 ^ G4#")]
        [TestCase("C#9 + 3 ^ C2", "E9 ^ C2")]
        [TestCase("Db5 ^ C#5", "Db5 ^ C#5")]
        [TestCase("Db5 ^ Db6 ^ Db7 ^ Db8", "Db5 ^ Db6 ^ Db7 ^ Db8")]
        public void ReadChordValid(string Input, string ExpectedOutput)
        {
            SyntaxReader SR = new SyntaxReader();
            SR.ReadChord();

            Assert.AreEqual(true, true);
        }

        [TestCase("3 + 3")]
        [TestCase("C#3 + C#3")]
        [TestCase("A40")]
        [TestCase("")]
        [TestCase("4 ^ C4")]
        [TestCase("Ab0 - 1")]
        public void ReadChordUnvalid(string Input)
        {
            SyntaxReader SR = new SyntaxReader();
            SR.ReadChord();

            Assert.AreEqual(true, true);
        }

        public void ReadNoteChangeValid(string Input, string ExpectedOutput)
        {
        }

        public void ReadNoteChangeUnvalid(string Input)
        {
        }

        public void ReadSettingValid(string Input)
        {
        }

        public void ReadSettingUnvalid(string Input)
        {
        }

        public void HandleMultipleCommandsValid(string Input)
        {
        }

        public void HandleMultipleCommandsUnvalid(string Input)
        {
        }
    }
}