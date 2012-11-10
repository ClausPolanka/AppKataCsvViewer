﻿using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class FirstPageCommandTest
    {
        [Test]
        public void Execute_WhenCommandIsExecuted_FirstPageWillBeShown()
        {
            var display = Substitute.For<Display>();
            var browsable = Substitute.For<Browsable>();
            var sut = new FirstPageCommand(display, browsable);

            sut.Execute();

            browsable.Received(1).FirstPage();
            display.Received(1).Show(Arg.Any<Page>());
        }

        [Test]
        public void IsNotExitCommand_GivenCommand_ReturnsTrue()
        {
            var sut = new FirstPageCommand(null, null);

            Assert.IsTrue(sut.IsNotExitCommand(), "is exit command");
        }

    }
}
