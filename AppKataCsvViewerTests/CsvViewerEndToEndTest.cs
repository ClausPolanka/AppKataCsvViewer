using System;
using System.IO;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerTests
{
    [TestFixture]
    public class CsvViewerEndToEndTest
    {
        [Test]
        public void ForADefaultPageSizeOf3_ViewerShowsOnePageForGivenCsvData()
        {
            var csvContent = new[] 
            { 
                "Name;Age;City", 
                "Peter;42;New York", 
                "Paul;57;London", 
                "Mary;35;Munich" 
            };

            File.WriteAllLines("persons.csv", csvContent);

            MainEntryPoint.Main(new[] { "persons.csv" });

            // Assert against redirected console output.

            File.Delete("persons.csv");
        }

        [SetUp]
        public virtual void SetUp()
        {
            // Redirect Console.Out = ??;
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.Out.Close();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
    }
}
