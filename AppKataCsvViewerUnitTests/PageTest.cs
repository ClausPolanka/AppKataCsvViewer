using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void MaxColumnLengths_GivenAPageWith3Records_ReturnMaxLengthForEachColumn()
        {
            var header = new DataRecord();
            header.Add("col_1_Maximum");
            header.Add("col_2");
            header.Add("col_3");

            var dataRecord_1 = new DataRecord();
            dataRecord_1.Add("val_1");
            dataRecord_1.Add("val_2_Maximum");
            dataRecord_1.Add("val_3");
            
            var dataRecord_2 = new DataRecord();
            dataRecord_2.Add("val_1");
            dataRecord_2.Add("val_2");
            dataRecord_2.Add("val_3_Maximum");

            var sut = new Page();
            sut.Add(header);
            sut.Add(dataRecord_1);
            sut.Add(dataRecord_2);

            int[] expectedMaxLengths = { "col_1_Maximum".Length, "val_2_Maximum".Length, "val_3_Maximum".Length };

            Assert.AreEqual(expectedMaxLengths, sut.MaxColumnLengths(), "page max column lengths");
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

        [Test]
        public void DataRecords_GivenTwoIdenticalDataRecordsForTwoPages_PageDataRecordsAreEqual()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("Field");

            var page1 = new Page();
            page1.Add(dataRecord);

            var page2 = new Page();
            page2.Add(dataRecord);

            Assert.That(page1.DataRecords, Is.EqualTo(page2.DataRecords), "page's data records must be equal");
        }
    }
}
