using AppKataCsvViewer;
using NSubstitute;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class CsvUserCommandsTests
    {
        [Test]
        public void FirstPage_GivenCsvUserCommandsWhenFirstPageIsCalled_DisplayShowsAPageAndPrintsUserOptions()
        {
            var browsable = Substitute.For<Browsable>();
            var display = Substitute.For<Display>();
            var sut = new CsvUserCommands(display, browsable);

            sut.FirstPage();

            display.Received(1).Show(Arg.Any<Page>());
            display.Received(1).PrintUserOptionsFor(Arg.Any<int>());
        } 
        
        [Test]
        public void FirstPage_GivenCsvUserCommandsWhenFirstPageIsCalled_BrowsableReturnsNextPage()
        {
            var browsable = Substitute.For<Browsable>();
            var display = Substitute.For<Display>();
            var sut = new CsvUserCommands(display, browsable);

            sut.FirstPage();

            browsable.Received(1).NextPage();
        }

        [TestCase("N")]
        [TestCase("Next")]
        [TestCase("P")]
        [TestCase("Previous")]
        [TestCase("F")]
        [TestCase("First")]
        [TestCase("L")]
        [TestCase("Last")]
        public void Execute_GivenCommand_ExpectedUserCommandIsExecuted(string command)
        {
            var browsable = Substitute.For<Browsable>();
            var display = Substitute.For<Display>();
            var sut = new CsvUserCommands(display, browsable);

            sut.Execute(command);

            display.Received(1).Show(Arg.Any<Page>());
            display.Received(1).PrintUserOptionsFor(Arg.Any<int>());
        }
    }
}