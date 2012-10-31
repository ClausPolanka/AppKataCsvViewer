using System;
using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class CsvViewerTest
    {
        [TestCase("x")]
        [TestCase("exit")]
        public void Show_UserEntersExit_ShowsFirstPageAndExits(string exitCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);
            
            cmdStub.ReceiveUserCommand().Returns(exitCommand);

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.DidNotReceive().Execute(Arg.Any<string>());
        }

        [TestCase("n", "x")]
        [TestCase("next", "x")]
        [TestCase("n", "exit")]
        [TestCase("next", "exit")]
        public void Show_UserEntersNext_ShowsNextPageAndExits(string nextCommand, string exitCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);

            cmdStub.ReceiveUserCommand().Returns(nextCommand, exitCommand);

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(nextCommand);
        }

        [TestCase("p", "x")]
        [TestCase("previous", "exit")]
        public void Show_UserEntersPrevious_ShowsPreviousPageAndExits(string previousCommand, string exitCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);

            cmdStub.ReceiveUserCommand().Returns(previousCommand, exitCommand);

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(previousCommand);
        }

        [TestCase("l", "x")]
        [TestCase("last", "exit")]
        public void Show_UserEntersLast_ShowsLastPageAndExits(string lastCommand, string exitCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);

            cmdStub.ReceiveUserCommand().Returns(lastCommand, exitCommand);

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(lastCommand);
        }

        [TestCase("f", "x")]
        [TestCase("first", "exit")]
        public void Show_UserEntersFirst_ShowsFirstPageAndExits(string firstCommand, string exitCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);

            cmdStub.ReceiveUserCommand().Returns(firstCommand, exitCommand);

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(firstCommand);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Show_UserEntersWrongCommand_CsvViewerShowsTableAndThrows(string wrongCommand)
        {
            var userCommands = Substitute.For<UserCommands>();
            var cmdStub = Substitute.For<UserCommandReceiver>();
            var sut = new CsvViewer(cmdStub, userCommands);

            cmdStub.ReceiveUserCommand().Returns(wrongCommand);
            
            Assert.Throws<Exception>(() => sut.Execute());
        }
    }
}
