using System;
using System.Collections.Generic;
using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class CsvViewerTest
    {
        private static Table DUMMY_TABLE = DUMMY_TABLE = new Table(new List<DataRecord>{new DataRecord(), new DataRecord()}, defaultPageSize: 1);

        [TestCase("x")]
        [TestCase("exit")]
        public void Show_GivenATableAndUserEntersExit_CsvViewerShowsTableViaDisplayAndExits(string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, display);
            
            cmdStub.ReceiveUserCommand().Returns(exitCommand);

            sut.Show(DUMMY_TABLE);

            display.Received().PrintUserOptionsFor(DUMMY_TABLE.PageCount);
            display.Received().Show(DUMMY_TABLE.NextPage());
        }

        [TestCase("n", "x")]
        [TestCase("next", "x")]
        [TestCase("n", "exit")]
        [TestCase("next", "exit")]
        public void Show_GivenATableAndUserEntersNext_CsvViewerShowsTableViaDisplayAndExits(string nextCommand, string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            cmdStub.ReceiveUserCommand().Returns(nextCommand,exitCommand);
            var cmdReceiver = cmdStub;
            var sut = new CsvViewer(cmdReceiver, display);

            sut.Show(DUMMY_TABLE);

            display.Received(2 /* Times */).Show(DUMMY_TABLE.NextPage());
        }

        [TestCase("")]
        [TestCase(null)]
        public void Show_GivenATableAndUserEntersWrongCommand_CsvViewerShowsTableAndThrows(string wrongCommand)
        {
            var display = Substitute.For<Display>();
            var cmdReceiver = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdReceiver, display);

            cmdReceiver.ReceiveUserCommand().Returns(wrongCommand);
            
            Assert.Throws<Exception>(() => sut.Show(DUMMY_TABLE));
        }
    }
}
