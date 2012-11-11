using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class NextPageCommandTest
    {
        [Test]
        public void Execute_WhenCommandIsExecuted_NextPageWillBeShown()
        {
            var display = Substitute.For<Display>();
            var browsable = Substitute.For<Browsable>();
            var sut = new NextPageCommand(display, browsable);

            sut.Execute();

            browsable.Received(1).NextPage();
            display.Received(1).Show(Arg.Any<Page>());
        }

        [Test]
        public void IsNotExitCommand_GivenCommand_ReturnsTrue()
        {
            var sut = new NextPageCommand(null, null);

            Assert.IsTrue(sut.IsNotExitCommand(), "is exit command");
        }

    }
}
