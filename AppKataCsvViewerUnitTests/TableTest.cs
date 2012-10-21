using System;
using System.Collections.Generic;
using System.Linq;
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
        public void NextPage_GivenDataRecordsAndPageSize_ReturnsNextPage()
        {
            var sut = new Table(Create10Records(), defaultPageSize: 3);
            
            Page nextPage = sut.NextPage();
            Assert.That(nextPage, Is.EqualTo(sut.Pages[0]), "table's page");
            
            nextPage = sut.NextPage();
            Assert.That(nextPage, Is.EqualTo(sut.Pages[1]), "table's page");
            
            nextPage = sut.NextPage();
            Assert.That(nextPage, Is.EqualTo(sut.Pages[2]), "table's page");
            
            nextPage = sut.NextPage();
            Assert.That(nextPage, Is.EqualTo(sut.Pages[3]), "table's page");
            
            nextPage = sut.NextPage();
            Assert.That(nextPage, Is.EqualTo(sut.Pages[0]), "table's page");
        }

        [Test]
        public void Create_GivenDataRecords_InitializePages()
        {
            List<DataRecord> dataRecords = new List<DataRecord> { RecordFor(field: "header") };

            for (int i = 1; i <= 6; i++)
                dataRecords.Add(RecordFor(i));

            dataRecords.Add(RecordFor(field: "lastPage"));

            var sut = new Table(dataRecords, defaultPageSize: 3);

            Assert.That(sut.PageCount, Is.EqualTo(3), "table page count");
            Assert.That(sut.Header, Is.EqualTo(RecordFor(field: "header")), "table's page header");
            Assert.That(sut.Pages[0], Is.EqualTo(PageContainsRecords(@from: 1, to: 3)), "table's first page");
            Assert.That(sut.Pages[1], Is.EqualTo(PageContainsRecords(@from: 4, to: 6)), "table's second page");
            Assert.That(sut.Pages[2], Is.EqualTo(ExpectedLastPage()), "table's last page");
        }

        [Test]
        public void Create_GivenNoRecords_Throws()
        {
            Assert.Throws<Exception>(() => new Table(new List<DataRecord>(), defaultPageSize: 1), "table's data records must not be empty");
            Assert.Throws<Exception>(() => new Table(null, defaultPageSize: 1), "table's data records must not be null"); 
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Create_GivenADefaultInvalidDefaultPage_Throws(int invalidPageSize)
        {
            Assert.Throws<Exception>(() => { new Table(DataRecords(), invalidPageSize); }, "table's default page size must be higher than 1");
        }

        private List<DataRecord> Create10Records()
        {
            DataRecord header = new DataRecord();

            List<DataRecord> records = new List<DataRecord> { header };

            for (int i = 1; i <= 10; i++)
                records.Add(RecordFor(i));
            return records;
        }

        private DataRecord RecordFor(int index = 0, string field = "field")
        {
            var record = new DataRecord();
            record.Add(field + index);
            record.Add(field + index);
            record.Add(field + index);
            return record;
        }

        private Page PageContainsRecords(int @from, int to)
        {
            var page = CreatePage();
            page.Add(RecordFor(field: "header"));
            
            for (int i = @from; i <= to; i++)
                page.Add(RecordFor(i));

            return page;
        }

        private Page ExpectedLastPage()
        {
            var page = CreatePage();
            page.Add(RecordFor(field: "header"));
            page.Add(RecordFor(field: "lastPage"));
            return page;
        }

        private Page CreatePage()
        {
            return new Page(new PageConsoleFormatter(new MaxConsoleColumnLengthsIdentifier()));
        }

        private List<DataRecord> DataRecords()
        {
            var dataRecords = new List<DataRecord> { new DataRecord() };
            return dataRecords;
        }
    }
}