using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void MaxColumnLengths_GivenAPageWith2Records_ReturnMaxLengthForEachColumn()
        {
            var header = new DataRecord();
            header.Add("Name");
            header.Add("Age");
            header.Add("City");

            var dataRecord = new DataRecord();
            dataRecord.Add("Claus");
            dataRecord.Add("30");
            dataRecord.Add("Vienna");

            var page = new Page();
            page.Add(header);
            page.Add(dataRecord);

            int[] expected = { "Claus".Length, "Age".Length, "Vienna".Length };

            Assert.AreEqual(expected, page.MaxColumnLengths(), "page max column lengths");
        }

        [Test]
        public void Equals_GivenTwoIdenticalDataRecordsForTwoPages_PagesAreEqual()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("Word");

            var page1 = new Page();
            page1.Add(dataRecord);

            var page2 = new Page();
            page2.Add(dataRecord);

            Assert.That(page1, Is.EqualTo(page2), "pages must be equal");
        }

        [Test]
        public void DataRecords_GivenTwoIdenticalDataRecordsForTwoPages_PageDataRecordsAreEqual()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("Word");

            var page1 = new Page();
            page1.Add(dataRecord);

            var page2 = new Page();
            page2.Add(dataRecord);

            Assert.That(page1.DataRecords, Is.EqualTo(page2.DataRecords), "page data records must be equal");
        }
    }
}
