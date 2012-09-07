using System;
using System.IO;
using NUnit.Framework;

namespace AppKataCsvViewerSpikes
{
    [TestFixture]
    public class ReadFromStandardInTest
    {
        [Test]
        public void ReadFromStandardIn()
        {
            Console.Write("> ");
            TextReader stdin = Console.In;
            Console.SetIn(new StringReader("Exit"));
            string line = Console.ReadLine();
            Console.Out.WriteLine(line);
            Console.SetIn(stdin);
        }
    }
}