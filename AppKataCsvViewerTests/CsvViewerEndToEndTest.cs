﻿using System;
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
        private TextReader stdin;
        private ApplicationTestExecutor csvViewerRunner = new ApplicationTestExecutor();

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

            UserWillEnterExitCommand();

            csvViewerRunner.ExecuteViewerFor(CSV_FILE_NAME);

            var expected = "Name |Age|City    |" + NEW_LINE + 
                           "-----+---+--------+" + NEW_LINE +
                           "Peter|42 |New York|" + NEW_LINE +
                           "Paul |57 |London  |" + NEW_LINE +
                           "Mary |35 |Munich  |" + NEW_LINE + NEW_LINE +
                           "eX(it"               + NEW_LINE;

            // TODO: user command must be called at this point in time. (Maybe use async Q)

            Assert.That(generatedCsvOutput.ToString, Is.EqualTo(expected), "formatted csv output");
        }

        private static void CreateTemporaryCsvFileWith(string[] csvContent)
        {
            File.WriteAllLines(CSV_FILE_NAME, csvContent);
        }

        private void UserWillEnterExitCommand()
        {
            Console.SetIn(new StringReader("x"));
        }

        [SetUp]
        public virtual void SetUp()
        {
            stdin = Console.In;
            stdout = Console.Out;
            generatedCsvOutput = new StringWriter();
            Console.SetOut(generatedCsvOutput);
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
