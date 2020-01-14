using ChordGenerator;
using NUnit.Framework;

namespace Tests
{
    class Tests_SyntaxReader
    {



       [TestCase()]
       public void ReadChordValid()
       {
            SyntaxReader SR = new SyntaxReader();

            Assert.AreEqual(true, true);
       }

        public void ReadChordUnvalid()
        {
            SyntaxReader SR = new SyntaxReader();

            Assert.AreEqual(true, true);
        }

    }
}
