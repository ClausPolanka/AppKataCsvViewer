using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PreviousPageCommandTest
    {
        [Test]
        public void Execute_WhenCommandIsExecuted_PreviousPageWillBeShown()
        {
            var display = Substitute.For<Display>();
            var browsable = Substitute.For<Browsable>();
            var sut = new PreviousPageCommand(display, browsable);

            sut.Execute();

            browsable.Received(1).PreviousPage();
            display.Received(1).Show(Arg.Any<Page>());
        }

        [Test]
        public void IsNotExitCommand_GivenCommand_ReturnsTrue()
        {
            var sut = new PreviousPageCommand(null, null);

            Assert.IsTrue(sut.IsNotExitCommand(), "is exit command");
        }

    }
}
