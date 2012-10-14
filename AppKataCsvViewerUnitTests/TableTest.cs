using System.Collections.Generic;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
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

            var sut = new Table(records, pageSize);

            Assert.That(sut.PageCount, Is.EqualTo(expectedPageCount), "table page count");
            Assert.That(sut.Header, Is.EqualTo(header), "table header");
        }

        [Test]
        public void Create_GivenDataRecords_InitializePages()
        {
            List<DataRecord> dataRecords = new List<DataRecord>();
            dataRecords.Add(Header());
            dataRecords.Add(FirstPageRecord1());
            dataRecords.Add(FirstPageRecord2());
            dataRecords.Add(FirstPageRecord3());
            dataRecords.Add(SecondPageRecord1());
            dataRecords.Add(SecondPageRecord2());
            dataRecords.Add(SecondPageRecord3());
            dataRecords.Add(LastPageRecord());

            var sut = new Table(dataRecords, defaultPageSize: 3);

            Assert.That(sut.PageCount, Is.EqualTo(3), "table page count");
            Assert.That(sut.Header, Is.EqualTo(ExpectedHeader()), "table page header");
            Assert.That(sut.Pages[0], Is.EqualTo(ExpectedFirstPage()), "table first page");
            Assert.That(sut.Pages[1], Is.EqualTo(ExpectedSecondPage()), "table second page");
            Assert.That(sut.Pages[2], Is.EqualTo(ExpectedLastPage()), "table last page");
        }

        private static DataRecord Header()
        {
            var header = new DataRecord();
            header.Add("Name");
            header.Add("Age");
            header.Add("City");
            return header;
        }

        private static DataRecord FirstPageRecord1()
        {
            var firstPageRecord1 = new DataRecord();
            firstPageRecord1.Add("Peter");
            firstPageRecord1.Add("42");
            firstPageRecord1.Add("New York");
            return firstPageRecord1;
        }

        private static DataRecord FirstPageRecord2()
        {
            var firstPageRecord2 = new DataRecord();
            firstPageRecord2.Add("Paul");
            firstPageRecord2.Add("57");
            firstPageRecord2.Add("London");
            return firstPageRecord2;
        }

        private static DataRecord FirstPageRecord3()
        {
            var firstPageRecord3 = new DataRecord();
            firstPageRecord3.Add("Mary");
            firstPageRecord3.Add("35");
            firstPageRecord3.Add("Munich");
            return firstPageRecord3;
        }

        private static DataRecord SecondPageRecord1()
        {
            var secondPageRecord1 = new DataRecord();
            secondPageRecord1.Add("Jaques");
            secondPageRecord1.Add("66");
            secondPageRecord1.Add("Paris");
            return secondPageRecord1;
        }

        private static DataRecord SecondPageRecord2()
        {
            var secondPageRecord2 = new DataRecord();
            secondPageRecord2.Add("Yuri");
            secondPageRecord2.Add("23");
            secondPageRecord2.Add("Moscow");
            return secondPageRecord2;
        }

        private static DataRecord SecondPageRecord3()
        {
            var secondPageRecord3 = new DataRecord();
            secondPageRecord3.Add("Stephanie");
            secondPageRecord3.Add("47");
            secondPageRecord3.Add("Stockholm");
            return secondPageRecord3;
        }

        private static DataRecord LastPageRecord()
        {
            var lastPageRecord = new DataRecord();
            lastPageRecord.Add("Nadia");
            lastPageRecord.Add("29");
            lastPageRecord.Add("Madrid");
            return lastPageRecord;
        }

        private static DataRecord ExpectedHeader()
        {
            var expectedHeader = new DataRecord();
            expectedHeader.Add("Name");
            expectedHeader.Add("Age");
            expectedHeader.Add("City");
            return expectedHeader;
        }

        private static Page ExpectedFirstPage()
        {
            var expectedFirstPage = new Page();
            expectedFirstPage.Add(Header());
            expectedFirstPage.Add(FirstPageRecord1());
            expectedFirstPage.Add(FirstPageRecord2());
            expectedFirstPage.Add(FirstPageRecord3());
            return expectedFirstPage;
        }

        private static Page ExpectedSecondPage()
        {
            var expectedSecondPage = new Page();
            expectedSecondPage.Add(Header());
            expectedSecondPage.Add(SecondPageRecord1());
            expectedSecondPage.Add(SecondPageRecord2());
            expectedSecondPage.Add(SecondPageRecord3());
            return expectedSecondPage;
        }

        private static Page ExpectedLastPage()
        {
            var expectedLastPage = new Page();
            expectedLastPage.Add(Header());
            expectedLastPage.Add(LastPageRecord());
            return expectedLastPage;
        }
    }
}