using System;
using System.IO;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerTests
{
    [TestFixture]
    public class CsvViewerEndToEndTest
    {
        private const string NEW_LINE = "\r\n";
        private const string CSV_FILE_NAME = "persons.csv";
        private StringWriter generatedCsvOutput;
        private TextWriter stdout;

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

            CreateTemporaryCsvFileWith(csvContent);

            MainEntryPoint.Main(new[] { CSV_FILE_NAME });

            var expected = "Name |Age|City    |" + NEW_LINE + 
                           "-----+---+--------+" + NEW_LINE +
                           "Peter|42 |New York|" + NEW_LINE +
                           "Paul |57 |London  |" + NEW_LINE +
                           "Mary |35 |Munich  |" + NEW_LINE;

           // TODO: Add empty row + user options

            Assert.That(generatedCsvOutput.ToString, Is.EqualTo(expected), "formatted csv output");
        }

        private static void CreateTemporaryCsvFileWith(string[] csvContent)
        {
            File.WriteAllLines(CSV_FILE_NAME, csvContent);
        }

        [SetUp]
        public virtual void SetUp()
        {
            stdout = Console.Out;
            generatedCsvOutput = new StringWriter();
            Console.SetOut(generatedCsvOutput);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.SetOut(stdout);
            generatedCsvOutput.Close();
            File.Delete(CSV_FILE_NAME);
        }
    }
}
