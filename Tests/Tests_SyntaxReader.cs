using ChordGenerator;
using NUnit.Framework;
using System;

namespace Tests
{
    internal class Tests_SyntaxReader
    {
        /// <summary>
        /// Creates the chord and uses its ToString().
        /// </summary>
        [TestCase("C#3", "C#3")]
        [TestCase("Db5 ^ C#5", "C#5 ^ C#5")]
        [TestCase("Db5 ^ Db6 ^ Db7 ^ Db8", "C#5 ^ C#6 ^ C#7 ^ C#8")]
        public void ReadChordValid(string Input, string ExpectedOutput)
        {
            SyntaxReader SR = new SyntaxReader(new RuntimeManager("Test"));
            Chord a = SR.ReadChord(Input);
            string Output = a.ToString().Trim('^', ' ');

            Assert.AreEqual(ExpectedOutput, Output);
        }
    }
}