using ChordGenerator;
using NUnit.Framework;

namespace Tests
{
    public class Tests_ContentDefinitions
    {
        /// <summary>
        /// Test names only
        /// </summary>
        /// <param name="a"></param>
        [TestCase("C#3")]
        [TestCase("C3")]
        [TestCase("Cb3")]
        [TestCase("C0")]
        [TestCase("C9")]
        public void CheckNoteNameValid(string a)
        {          
            Assert.AreEqual(MusicalNote.IsValidName(a), true);
        }

        /// <summary>
        /// Test names only
        /// </summary>
        /// <param name="a"></param>
        [TestCase("C3b")]
        [TestCase("CC")]
        [TestCase("C")]
        [TestCase("Cb0")]
        [TestCase("A0")]
        [TestCase("CCCC")]
        [TestCase("K3")]
        [TestCase("3C")]
        [TestCase("C#9")]
        public void CheckNoteNameUnvalid(string a)
        {
            Assert.AreEqual(MusicalNote.IsValidName(a), false);
        }

        [TestCase(16)]
        [TestCase(20000)]
        [TestCase(616)]
        public void CheckFrequencyValid(int a)
        {
            Assert.AreEqual(MusicalNote.IsValidFrequency(a), true);
        }

        [TestCase(-16)]
        [TestCase(20002)]
        public void CheckFrequencyUnvalid(int a)
        {
            Assert.AreEqual(MusicalNote.IsValidFrequency(a), false);
        }
    }
}