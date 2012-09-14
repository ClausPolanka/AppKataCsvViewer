using System.Collections.Generic;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerTests
{
    [TestFixture]
    public class TableTest
    {
        [TestCase(10, 4, 3)]
        [TestCase(4, 3, 2)]
        [TestCase(1, 2, 1)]
        public void Create_GivenDataRecordCountAndPageSize_PageCountAndHeaderWillBeSet(int dataRecordCount, int pageSize, int expectedPageCount)
        {
            DataRecord header = new DataRecord();

            List<DataRecord> records = new List<DataRecord> { header };

            for (int i = 0; i < dataRecordCount; i++)
                records.Add(new DataRecord());

            var table = new Table(records, pageSize);

            Assert.That(table.PageCount, Is.EqualTo(expectedPageCount), "table page count");
            Assert.That(table.Header, Is.EqualTo(header), "table header");
        }

        [Test]
        public void Create_GivenDataRecords_InitializePages()
        {
            List<DataRecord> dataRecords = new List<DataRecord>();

            var header = new DataRecord();
            header.Add("Name");
            header.Add("Age");
            header.Add("City");
            
            dataRecords.Add(header);

            var firstPageRecord1 = new DataRecord();
            firstPageRecord1.Add("Peter"); 
            firstPageRecord1.Add("42"); 
            firstPageRecord1.Add("New York");

            dataRecords.Add(firstPageRecord1);
            
            var firstPageRecord2 = new DataRecord();
            firstPageRecord2.Add("Paul"); 
            firstPageRecord2.Add("57"); 
            firstPageRecord2.Add("London");

            dataRecords.Add(firstPageRecord2);

            var firstPageRecord3 = new DataRecord();
            firstPageRecord3.Add("Mary");
            firstPageRecord3.Add("35");
            firstPageRecord3.Add("Munich");

            dataRecords.Add(firstPageRecord3);

            var secondPageRecord1 = new DataRecord();
            secondPageRecord1.Add("Jaques");
            secondPageRecord1.Add("66");
            secondPageRecord1.Add("Paris");

            dataRecords.Add(secondPageRecord1);

            var secondPageRecord2 = new DataRecord();
            secondPageRecord2.Add("Yuri");
            secondPageRecord2.Add("23");
            secondPageRecord2.Add("Moscow");

            dataRecords.Add(secondPageRecord2);

            var secondPageRecord3 = new DataRecord();
            secondPageRecord3.Add("Stephanie");
            secondPageRecord3.Add("47");
            secondPageRecord3.Add("Stockholm");

            dataRecords.Add(secondPageRecord3);

            var lastPageRecord = new DataRecord();
            lastPageRecord.Add("Nadia");
            lastPageRecord.Add("29");
            lastPageRecord.Add("Madrid");

            dataRecords.Add(lastPageRecord);

            var table = new Table(dataRecords, pageSize: 3);

            var expectedHeader = new DataRecord();
            expectedHeader.Add("Name");
            expectedHeader.Add("Age");
            expectedHeader.Add("City");

            Assert.That(table.PageCount, Is.EqualTo(3), "table page count");
            Assert.That(table.Header, Is.EqualTo(expectedHeader), "table page header");

            var expectedFirstPage = new Page();
            expectedFirstPage.Add(header);
            expectedFirstPage.Add(firstPageRecord1);
            expectedFirstPage.Add(firstPageRecord2);
            expectedFirstPage.Add(firstPageRecord3);

            Assert.That(table.Pages[0], Is.EqualTo(expectedFirstPage), "table first page");

            var expectedSecondPage = new Page();
            expectedSecondPage.Add(header);
            expectedSecondPage.Add(secondPageRecord1);
            expectedSecondPage.Add(secondPageRecord2);
            expectedSecondPage.Add(secondPageRecord3);

            Assert.That(table.Pages[1], Is.EqualTo(expectedSecondPage), "table second page");

            var expectedLastPage = new Page();
            expectedLastPage.Add(header);
            expectedLastPage.Add(lastPageRecord);

            Assert.That(table.Pages[2], Is.EqualTo(expectedLastPage), "table last page");
        }

    }
}