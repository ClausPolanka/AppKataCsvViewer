using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageSizeAgentTest
    {
        private const int IGNORE = 1;

        [Test]
        public void DetectPageSize_GivenNoCustomPageSize_ReturnsDefaultPageSize() {
            var sut = new PageSizeAgent(defaultPageSize: 3, indexOfPageSize: 1);

            int pageSize = sut.DetectPageSize(new[] { "csvFile" });

            Assert.That(pageSize, Is.EqualTo(3), "page size");
        }

        [Test]
        public void DetectPageSize_GivenCustomPageSize_ReturnsDefaultPageSize() {
            var sut = new PageSizeAgent(IGNORE, indexOfPageSize: 1);

            int pageSize = sut.DetectPageSize(new[] { "csvFile", "5" });

            Assert.That(pageSize, Is.EqualTo(5), "page size");
        }

        [Test]
        public void DetectPageSize_GivenNotANumberForCustomPageSize_ReturnsDefaultPageSize() {
            var sut = new PageSizeAgent(defaultPageSize: 3, indexOfPageSize: 1);

            int pageSize = sut.DetectPageSize(new[] { "csvFile", "NaN" });

            Assert.That(pageSize, Is.EqualTo(3), "page size");
        }

        [Test]
        public void DetectPageSize_GivenNullAsArgs_ReturnsDefaultPageSize() {
            var sut = new PageSizeAgent(3 /* default page size */, IGNORE);

            int pageSize = sut.DetectPageSize(null);

            Assert.That(pageSize, Is.EqualTo(3), "page size");
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void DetectPageSize_GivenNegativeDefaultPageSize_Throws(int defaultPageSize) {
            Assert.Throws<PageSizeAgent.DefaultPageSizeMustBeHigherThanZero>(() =>
            {
                new PageSizeAgent(defaultPageSize, IGNORE);
            });
        }
        
        [Test]
        public void DetectPageSize_GivenNegativeInxdexForCustomPageSize_Throws() {
            Assert.Throws<PageSizeAgent.NegativeIndexOfCustomPageSizeNotAllowed>(() =>
            {
                new PageSizeAgent(IGNORE, -1);
            });
        }
    }
}