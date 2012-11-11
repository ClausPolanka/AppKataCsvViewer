using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class LastPageCommandTest
    {
        [Test]
        public void Execute_WhenCommandIsExecuted_LastPageWillBeShown()
        {
            var display = Substitute.For<Display>();
            var browsable = Substitute.For<Browsable>();
            var sut = new LastPageCommand(display, browsable);

            sut.Execute();

            browsable.Received(1).LastPage();
            display.Received(1).Show(Arg.Any<Page>());
        }

        [Test]
        public void IsNotExitCommand_GivenCommand_ReturnsTrue()
        {
            var sut = new LastPageCommand(null, null);

            Assert.IsTrue(sut.IsNotExitCommand(), "is exit command");
        }

    }
}
