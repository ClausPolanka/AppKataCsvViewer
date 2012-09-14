using System;
using System.IO;
using NUnit.Framework;

namespace AppKataCsvViewerTests
{
    [TestFixture]
    public class CsvViewerEndToEndTest
    {
        private const string NEW_LINE = "\r\n";
        private const string CSV_FILE_NAME = "persons.csv";
        private const string NEXT_COMMAND = "n";
        private const string EXIT_COMMAND = "x";

        private StringWriter generatedCsvOutput;
        private TextWriter stdout;
        private TextReader stdin;
        private ApplicationTestExecutor csvViewerRunner;

        [Test]
        public void ForADefaultPageSizeOf3AndUserEntersExitCommand_ViewerShowsOnePageForGivenCsvDataAndExits()
        {
            var csvContent = new[] 
            { 
                "Name;Age;City", 
                "Peter;42;New York", 
                "Paul;57;London", 
                "Mary;35;Munich" 
            };

            CreateTemporaryCsvFileWith(csvContent);

            csvViewerRunner.ExecuteViewerFor(CSV_FILE_NAME);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var expected = "Name |Age|City    |" +
                           "-----+---+--------+" +
                           "Peter|42 |New York|" +
                           "Paul |57 |London  |" +
                           "Mary |35 |Munich  |" +
                           "eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(expected), "formatted csv output");
        }

        [Test]
        public void ForADefaultPageSizeOf3AndUserEntersExitCommand_ViewerShowsFirstPageForGivenCsvDataAndExits()
        {
            var csvContent = new[] 
            { 
                "Name;Age;City", 
                "Peter;42;New York", 
                "Paul;57;London", 
                "Mary;35;Munich",
                "Jaques;66;Paris",
                "Yuri;23;Moscow",
                "Stephanie;47;Stockholm",
                "Nadia;29;Madrid"
            };

            CreateTemporaryCsvFileWith(csvContent);

            csvViewerRunner.ExecuteViewerFor(CSV_FILE_NAME);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var expected = "Name |Age|City    |" +
                           "-----+---+--------+" +
                           "Peter|42 |New York|" +
                           "Paul |57 |London  |" +
                           "Mary |35 |Munich  |" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(expected), "formatted csv output");
        }

        [Test]
        public void ForADefaultPageSizeOf3AndUserEntersNextAndExitCommand_ViewerShowsSecondPageForGivenCsvDataAndExits()
        {
            var csvContent = new[] 
            { 
                "Name;Age;City", 
                "Peter;42;New York", 
                "Paul;57;London", 
                "Mary;35;Munich",
                "Jaques;66;Paris",
                "Yuri;23;Moscow",
                "Stephanie;47;Stockholm",
                "Nadia;29;Madrid"
            };

            CreateTemporaryCsvFileWith(csvContent);

            csvViewerRunner.ExecuteViewerFor(CSV_FILE_NAME);
            csvViewerRunner.ReadsUserCommmand(NEXT_COMMAND);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var nextPage = "Name     |Age|City     |" +
                           "---------+---+---------+" +
                           "Jaques   |66 |Paris    |" +
                           "Yuri     |23 |Moscow   |" +
                           "Stephanie|35 |Stockholm|" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(nextPage), "formatted csv output");
        }

        private static void CreateTemporaryCsvFileWith(string[] csvContent)
        {
            File.WriteAllLines(CSV_FILE_NAME, csvContent);
        }

        private string WithoutLineBreaks(StringWriter stringWriter)
        {
            return stringWriter.ToString().Replace(NEW_LINE, string.Empty);
        }

        [SetUp]
        public virtual void SetUp()
        {
            stdin = Console.In;
            stdout = Console.Out;
            generatedCsvOutput = new StringWriter();
            Console.SetOut(generatedCsvOutput);
            csvViewerRunner = new ApplicationTestExecutor(generatedCsvOutput);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.SetOut(stdout);
            Console.SetIn(stdin);
            generatedCsvOutput.Close();
            File.Delete(CSV_FILE_NAME);
        }
    }
}
