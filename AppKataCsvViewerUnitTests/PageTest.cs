using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        private const string NL = "\n";

        [Test]
        public void Header_GivenDataRecordWithThreeFields_ReturnsCorrectlyFormattedHeader()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("headerField_1");
            dataRecord.Add("headerField_2");
            dataRecord.Add("headerField_3");

            var sut = CreatePage();
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

            var sut = CreatePage();
            sut.Add(headerRecord);
            sut.Add(dataRecord);

            var expectedDataRecords = NL + "field_1      |field_2      |field_3      |" + NL;

            Assert.That(sut.DataRecords, Is.EqualTo(expectedDataRecords), "page data records");
        }

        [Test]
        public void Equals_GivenTwoIdenticalDataRecordsForTwoPages_PagesAreEqual()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("Field");

            var page1 = CreatePage();
            page1.Add(dataRecord);

            var page2 = CreatePage();
            page2.Add(dataRecord);

            Assert.That(page1, Is.EqualTo(page2), "pages must be equal");
        }

        [Test]
        public void DataRecords_GivenOneDataRecordContaingEmptyField_TranslateEmptyFieldToSpaces()
        {
            var headerRecord = new DataRecord();
            headerRecord.Add("headerField_1");
            headerRecord.Add("headerField_2");
            headerRecord.Add("headerField_3");

            var dataRecord = new DataRecord();
            dataRecord.Add("field_1");
            dataRecord.Add(string.Empty);
            dataRecord.Add("field_3");

            Page sut = CreatePage();
            sut.Add(headerRecord);
            sut.Add(dataRecord);
            
            string expectedDataRecords = NL + "field_1      |             |field_3      |" + NL;

            Assert.That(sut.DataRecords, Is.EqualTo(expectedDataRecords), "page data records");
        }

        private static Page CreatePage()
        {
            return new Page(new PageConsoleFormatter(new MaxConsoleColumnLengthsIdentifier()));
        }
    }
}
