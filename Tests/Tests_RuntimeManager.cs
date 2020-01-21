using ChordGenerator;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    internal class Tests_RuntimeManager
    {
        [TestCase(442)]
        [TestCase(440)]
        [TestCase(438)]
        [TestCase(436)]
        [TestCase(434)]
        [TestCase(432)]
        public void GenerateArrayValid(float frequency)
        {
            RuntimeManager RM = new RuntimeManager();

            RM.GenerateMusicalNoteArray("A4", frequency);

            Assert.AreEqual(true, RM.MusicalNotes[3] is MusicalNote);

        }

        [TestCase(450)]
        [TestCase(430)]
        [TestCase(428)]
        public void GenerateArrayUnvalid(float frequency)
        {
            RuntimeManager RM = new RuntimeManager();

            RM.GenerateMusicalNoteArray("A4", frequency);

            Assert.AreEqual(false, RM.MusicalNotes[3] is MusicalNote);
        }
    }
}