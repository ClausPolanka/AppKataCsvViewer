using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void Header_GivenDataRecordWithThreeFields_ReturnsCorrectlyFormattedHeader()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("headerField_1");
            dataRecord.Add("headerField_2");
            dataRecord.Add("headerField_3");

            var sut = new Page();
            sut.Add(dataRecord);

            var expectedHeader = "headerField_1|headerField_2|headerField_3|\n" +
                                 "-------------+-------------+-------------+";

            Assert.That(sut.Header(), Is.EqualTo(expectedHeader), "page header");
        }

        [Test]
        public void DataRecords_GivenTwoDataRecordsEachContainingThreeFields_ReturnsCorrectlyFormattedDataRecords()
        {
            var headerRecord = new DataRecord();
            headerRecord.Add("headerField_1");
            headerRecord.Add("headerField_2");
            headerRecord.Add("headerField_3");

            var dataRecord = new DataRecord();
            dataRecord.Add("field_1");
            dataRecord.Add("field_2");
            dataRecord.Add("field_3");

            var sut = new Page();
            sut.Add(headerRecord);
            sut.Add(dataRecord);

            var expectedDataRecords = "\nfield_1      |field_2      |field_3      |\n";

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
