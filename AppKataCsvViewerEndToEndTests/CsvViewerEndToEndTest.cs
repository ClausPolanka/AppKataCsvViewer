using System;
using System.IO;
using NUnit.Framework;

namespace AppKataCsvViewerEndToEndTests
{
    [TestFixture]
    public class CsvViewerEndToEndTest
    {
        private const string NEW_LINE = "\r\n";
        private const string CSV_FILE_NAME = "persons.csv";
        private const string NEXT_COMMAND = "N";
        private const string PREVIOUS_COMMAND = "P";
        private const string FIRST_COMMAND = "F";
        private const string LAST_COMMAND = "L";
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
                           "Stephanie|47 |Stockholm|" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(nextPage), "formatted csv output");
        }

        [Test]
        public void ForADefaultPageSizeOf3AndUserEntersNextNextAndExitCommand_ViewerShowsLastPageForGivenCsvDataAndExits()
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
            csvViewerRunner.ReadsUserCommmand(NEXT_COMMAND);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var nextPage = "Name |Age|City  |" +
                           "-----+---+------+" +
                           "Nadia|29 |Madrid|" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(nextPage), "formatted csv output");
        }

        [Test]
        public void ForADefaultPageSizeOf3AndUserReachesLastPageAndEntersNextCommand_ViewerShowsFirstPageForGivenCsvDataAndExits()
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
            csvViewerRunner.ReadsUserCommmand(NEXT_COMMAND);
            csvViewerRunner.ReadsUserCommmand(NEXT_COMMAND);
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
        public void GivenCsvContentAndADefaultPageSizeOf3_UserEntersNextPreviousAndExitCommand_TablesFirstPageWillBeShown()
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
            csvViewerRunner.ReadsUserCommmand(PREVIOUS_COMMAND);
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
        public void GivenCsvContentAndADefaultPageSizeOf3_UserEntersLastAndExitCommand_TablesLastPageWillBeShown()
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
            csvViewerRunner.ReadsUserCommmand(LAST_COMMAND);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var expected = "Name |Age|City  |" +
                           "-----+---+------+" +
                           "Nadia|29 |Madrid|" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(expected), "formatted csv output");
        }

        [Test]
        public void GivenCsvContentAndADefaultPageSizeOf3_UserEntersLastFirstAndExitCommand_TablesFirstPageWillBeShown()
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
            csvViewerRunner.ReadsUserCommmand(LAST_COMMAND);
            csvViewerRunner.ReadsUserCommmand(FIRST_COMMAND);
            csvViewerRunner.ReadsUserCommmand(EXIT_COMMAND);

            var expected = "Name |Age|City    |" +
                           "-----+---+--------+" +
                           "Peter|42 |New York|" +
                           "Paul |57 |London  |" +
                           "Mary |35 |Munich  |" +
                           "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

            Assert.That(WithoutLineBreaks(generatedCsvOutput), Is.EqualTo(expected), "formatted csv output");
        }

        private static void CreateTemporaryCsvFileWith(string[] csvContent)
        {
            File.WriteAllLines(CSV_FILE_NAME, csvContent);
        }

        private string WithoutLineBreaks(StringWriter stringWriter)
        {
            return stringWriter.ToString().Replace(NEW_LINE, string.Empty).Replace("\n", string.Empty);
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
