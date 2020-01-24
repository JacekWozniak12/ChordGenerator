using ChordGenerator;
using NUnit.Framework;
using System;

namespace Tests
{
    internal class Tests_Settings
    {
        [TestCase(442)]
        [TestCase(440)]
        [TestCase(438)]
        [TestCase(436)]
        [TestCase(434)]
        [TestCase(432)]
        public void GenerateArrayValid(float frequency)
        {
            var s = new Settings();
            s.GenerateMusicalNoteArray("A4", frequency);

            Assert.AreEqual(true, s.MusicalNotes[3] is MusicalNote);
        }

        [TestCase(2555)]
        [TestCase(430)]
        [TestCase(428)]
        public void GenerateArrayUnvalid(float frequency)
        {
            var s = new Settings();

            try
            {
                s.GenerateMusicalNoteArray("A4", frequency);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual
                ((int)440, (int)s.MusicalNotes.Find(x => x.Name == "A4").Frequency);
        }
    }
}