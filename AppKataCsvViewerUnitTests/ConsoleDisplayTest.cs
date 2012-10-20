using System;
using System.Collections.Generic;
using System.IO;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class ConsoleDisplayTest
    {
        private const string NL = "\n";
        private const string CR = "\r";

        private TextReader stdin;
        private TextWriter stdout;
        private StringWriter displayOutput;

        [Test]
        public void Show_GivenATableFilledWithDataRecordsFittingOnOnePage_ShowTableWithExitCommand()
        {
            Table table = new Table(ThreeDataRecords(), defaultPageSize: 3);

            var sut = new ConsoleDisplay();
            sut.Show(table);

            var expected = "Field1|Field2|Field3|" + NL +
                           "------+------+------+" + NL +
                           "Field1|Field2|Field3|" + NL +
                           "Field1|Field2|Field3|" + NL + 
                           CR + NL +
                           ConsoleDisplay.EXIT_COMMAND + CR + NL;

            Assert.That(displayOutput.ToString(), Is.EqualTo(expected), "display output");
        }

        [Test]
        public void Show_GivenATableFilledWithDataRecordsFittingOnTwoPages_ShowTableWithAllUserCommands()
        {
            Table table = new Table(ThreeDataRecords(), defaultPageSize: 1);

            var sut = new ConsoleDisplay();
            sut.Show(table);

            var expectedOutput = "Field1|Field2|Field3|" + NL +
                                 "------+------+------+" + NL +
                                 "Field1|Field2|Field3|" + NL + 
                                 CR + NL + 
                                 ConsoleDisplay.ALL_USER_COMMANDS + CR + NL;

            Assert.That(displayOutput.ToString(), Is.EqualTo(expectedOutput), "display output");
        }

        private List<DataRecord> ThreeDataRecords()
        {
            List<DataRecord> dataRecords = new List<DataRecord>();
            
            for (int i = 0; i < 3; i++)
            {
                DataRecord dr = new DataRecord();
                dr.Add("Field1");
                dr.Add("Field2");
                dr.Add("Field3");
                dataRecords.Add(dr);
            }

            return dataRecords;
        }

        [SetUp]
        public virtual void SetUp()
        {
            stdin = Console.In;
            stdout = Console.Out;
            displayOutput = new StringWriter();
            Console.SetOut(displayOutput);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.SetOut(stdout);
            Console.SetIn(stdin);
            displayOutput.Close();
        }
    }
}