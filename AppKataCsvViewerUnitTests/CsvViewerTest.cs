using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class CsvViewerTest
    {
        [Test]
        public void Show_UserEntersExit_ShowsFirstPageAndExits()
        {
            var userCommands = Substitute.For<UserCommands>();
            var userStub = Substitute.For<User>();
            var sut = new CsvViewer(userStub, userCommands);
            
            userStub.EnteredCommand().Returns(new ExitCommand());

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.DidNotReceive().Execute(Arg.Any<UserCommand>());
        }

        [Test]
        public void Show_UserEntersNext_ShowsNextPageAndExits()
        {
            var userCommands = Substitute.For<UserCommands>();
            var user = Substitute.For<User>();
            var sut = new CsvViewer(user, userCommands);

            user.EnteredCommand().Returns(new NextPageCommand(null, null), new ExitCommand());

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(new NextPageCommand(null, null));
            userCommands.DidNotReceive().Execute(new ExitCommand());
        }

        [Test]
        public void Show_UserEntersPrevious_ShowsPreviousPageAndExits()
        {
            var userCommands = Substitute.For<UserCommands>();
            var user = Substitute.For<User>();
            var sut = new CsvViewer(user, userCommands);

            user.EnteredCommand().Returns(new PreviousPageCommand(null, null), new ExitCommand());

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(new PreviousPageCommand(null, null));
            userCommands.DidNotReceive().Execute(new ExitCommand());
        }

        [Test]
        public void Show_UserEntersLast_ShowsLastPageAndExits()
        {
            var userCommands = Substitute.For<UserCommands>();
            var user = Substitute.For<User>();
            var sut = new CsvViewer(user, userCommands);

            user.EnteredCommand().Returns(new LastPageCommand(null, null), new ExitCommand());

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(new LastPageCommand(null, null));
            userCommands.DidNotReceive().Execute(new ExitCommand());
        }

        [Test]
        public void Show_UserEntersFirst_ShowsFirstPageAndExits()
        {
            var userCommands = Substitute.For<UserCommands>();
            var user = Substitute.For<User>();
            var sut = new CsvViewer(user, userCommands);

            user.EnteredCommand().Returns(new FirstPageCommand(null, null), new ExitCommand());

            sut.Execute();

            userCommands.Received(1).FirstPage();
            userCommands.Received(1).Execute(new FirstPageCommand(null, null));
            userCommands.DidNotReceive().Execute(new ExitCommand());

        }
    }
}
