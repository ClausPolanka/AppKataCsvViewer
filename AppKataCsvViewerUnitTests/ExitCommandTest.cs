using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class ExitPageCommandTest
    {
        [Test]
        public void IsNotExitCommand_GivenCommand_ReturnsTrue()
        {
            var sut = new ExitCommand();

            Assert.IsFalse(sut.IsNotExitCommand(), "is exit command");
        }

    }
}
