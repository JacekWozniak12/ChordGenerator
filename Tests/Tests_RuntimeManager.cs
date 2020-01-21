using ChordGenerator;
using NUnit.Framework;
using System;
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

        [TestCase(2555)]
        [TestCase(430)]
        [TestCase(428)]
        public void GenerateArrayUnvalid(float frequency)
        {
            RuntimeManager RM = new RuntimeManager();
           
            try
            {
                RM.GenerateMusicalNoteArray("A4", frequency);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual
                (0f, RM.MusicalNotes.Find(x => x.Name == "A4").Frequency);
        }
    }
}