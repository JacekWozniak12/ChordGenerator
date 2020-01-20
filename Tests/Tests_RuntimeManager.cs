using NUnit.Framework;

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
        public void GenerateArrayValid(float Frequency)
        {
            
        }

        [TestCase(450)]
        [TestCase(430)]
        [TestCase(428)]
        public void GenerateArrayUnvalid(float Frequency)
        {
            
        }
    }
}