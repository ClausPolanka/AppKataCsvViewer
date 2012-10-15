using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void Header_GivenDataRecordWithThreeFields_CreateHeader()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("headerColumn_1");
            dataRecord.Add("headerColumn_2");
            dataRecord.Add("headerColumn_3");

            var sut = new Page();
            sut.Add(dataRecord);

            var expectedHeader = "headerColumn_1|headerColumn_2|headerColumn_3|\n" +
                                 "--------------+--------------+--------------+";

            Assert.That(sut.Header(), Is.EqualTo(expectedHeader), "page header");
        }

        [Test]
        public void GetDataRecords_GivenOneDataRecordWithThreeFields_ReturnsDataRecordsCorrectlyFormatted()
        {
            var headerRecord = new DataRecord();
            headerRecord.Add("headerColumn_1");
            headerRecord.Add("headerColumn_2");
            headerRecord.Add("headerColumn_3");

            var dataRecord = new DataRecord();
            dataRecord.Add("value_1");
            dataRecord.Add("value_2");
            dataRecord.Add("value_3");

            var sut = new Page();
            sut.Add(headerRecord);
            sut.Add(dataRecord);

            var expectedDataRecords = "\nvalue_1       |value_2       |value_3       |\n";

            Assert.That(sut.DataRecords, Is.EqualTo(expectedDataRecords), "page data records");
        }

        [Test]
        public void Equals_GivenTwoIdenticalDataRecordsForTwoPages_PagesAreEqual()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("Field");

            var page1 = new Page();
            page1.Add(dataRecord);

            var page2 = new Page();
            page2.Add(dataRecord);

            Assert.That(page1, Is.EqualTo(page2), "pages must be equal");
        }

    }
}
