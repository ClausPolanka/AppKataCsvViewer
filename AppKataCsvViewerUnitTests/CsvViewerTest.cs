using System;
using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class CsvViewerTest
    {
        private static Page DUMMY_PAGE = new Page(new PageConsoleFormatter(new MaxConsoleColumnLengthsIdentifier()));
        private static int DUMMY_PAGECOUNT;

        [TestCase("x")]
        [TestCase("exit")]
        public void Show_UserEntersExit_ShowsFirstPageAndExits(string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var browsable = Substitute.For<Browsable>();
            var sut = new CsvViewer(cmdStub, display);
            
            cmdStub.ReceiveUserCommand().Returns(exitCommand);

            sut.Show(browsable);

            browsable.Received(1).NextPage();
            display.ReceivedWithAnyArgs(1).PrintUserOptionsFor(DUMMY_PAGECOUNT);
            display.ReceivedWithAnyArgs(1).Show(DUMMY_PAGE);
        }

        [TestCase("n", "x")]
        [TestCase("next", "x")]
        [TestCase("n", "exit")]
        [TestCase("next", "exit")]
        public void Show_UserEntersNext_ShowsNextPageAndExits(string nextCommand, string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var browsable = Substitute.For<Browsable>();
            var sut = new CsvViewer(cmdStub, display);
            
            cmdStub.ReceiveUserCommand().Returns(nextCommand,exitCommand);

            sut.Show(browsable);

            browsable.Received(2).NextPage();
            display.ReceivedWithAnyArgs(2).PrintUserOptionsFor(DUMMY_PAGECOUNT);
            display.ReceivedWithAnyArgs(2).Show(DUMMY_PAGE);
        }

        [TestCase("p", "x")]
        [TestCase("previous", "exit")]
        public void Show_UserEntersPrevious_ShowsPreviousPageAndExits(string previousCommand, string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var browsable = Substitute.For<Browsable>();
            var sut = new CsvViewer(cmdStub, display);
            
            cmdStub.ReceiveUserCommand().Returns(previousCommand, exitCommand);

            sut.Show(browsable);

            browsable.Received(1).NextPage();
            browsable.Received(1).PreviousPage();
            display.ReceivedWithAnyArgs(2).PrintUserOptionsFor(DUMMY_PAGECOUNT);
            display.ReceivedWithAnyArgs(2).Show(DUMMY_PAGE);
        }

        [TestCase("l", "x")]
        [TestCase("last", "exit")]
        public void Show_UserEntersLast_ShowsLastPageAndExits(string lastCommand, string exitCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var browsable = Substitute.For<Browsable>();
            var sut = new CsvViewer(cmdStub, display);
            
            cmdStub.ReceiveUserCommand().Returns(lastCommand, exitCommand);

            sut.Show(browsable);

            browsable.Received(1).NextPage();
            browsable.Received(1).LastPage();
            display.ReceivedWithAnyArgs(2).PrintUserOptionsFor(DUMMY_PAGECOUNT);
            display.ReceivedWithAnyArgs(2).Show(DUMMY_PAGE);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Show_UserEntersWrongCommand_CsvViewerShowsTableAndThrows(string wrongCommand)
        {
            var display = Substitute.For<Display>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var browsable = Substitute.For<Browsable>();
            var sut = new CsvViewer(cmdStub, display);

            cmdStub.ReceiveUserCommand().Returns(wrongCommand);
            
            Assert.Throws<Exception>(() => sut.Show(browsable));
        }
    }
}
