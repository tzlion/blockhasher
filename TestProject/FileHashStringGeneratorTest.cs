using System.IO;
using NUnit.Framework;
using BlockHasher;

namespace Tests
{
    public class FileHashStringGeneratorTest
    {
        [SetUp]
        public void Setup()
        {
            var stream = File.Create("testfile.bin");
            byte[] contents = {69, 69, 69, 69, 42, 42, 42, 42, 69, 69, 69, 69, 66};
            stream.Write(contents);
            stream.Close();
        }

        [Test]
        public void TestExpectedOutputIsGenerated()
        {
            var output = FileHashStringGenerator.BuildStringForFile("testfile.bin", 4);
            var expected = "File testfile.bin\r\nBlock size 4\r\n" 
                           + "6f96ba8525cc2f838c7d8b6888a1481b\r\n"
                           + "455831477b82574f6bf871193f2f761d\r\n" 
                           + "6f96ba8525cc2f838c7d8b6888a1481b\r\n"
                           + "bf7b5daa736fcf98a9491b53929ff05e\r\n";
            Assert.AreEqual(expected, output);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete("testfile.bin");
        }
    }
}