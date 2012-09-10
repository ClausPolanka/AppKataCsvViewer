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
    }
}