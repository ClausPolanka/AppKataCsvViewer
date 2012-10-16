using System.Collections.Generic;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class MaxConsoleColumnLengthsIdentifierTest
    {
        [Test]
        public void MaxColumnLengthsFor_GivenOneDataRecordContainingOneField_ReturnsFieldLengthAsMaxColumnLength()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("field");

            List<DataRecord> dataRecords = new List<DataRecord> { dataRecord };

            var sut = new MaxConsoleColumnLengthsIdentifier();

            Assert.That(sut.MaxColumnLengthsOf(dataRecords), Is.EqualTo(new[] { "field".Length }), "max column lengths");
        }
        
        [Test]
        public void MaxColumnLengthsFor_GivenOneDataRecordContainingTwoFields_ReturnsMaxColumnLengthsForTwoColumns()
        {
            var dataRecord = new DataRecord();
            dataRecord.Add("field");
            dataRecord.Add("fieldxxx");

            List<DataRecord> dataRecords = new List<DataRecord> { dataRecord };

            var sut = new MaxConsoleColumnLengthsIdentifier();

            var expectedMaxLengths = new[] { "field".Length, "fieldxxx".Length };
            
            Assert.That(sut.MaxColumnLengthsOf(dataRecords), Is.EqualTo(expectedMaxLengths), "max column lengths");
        }

        [Test]
        public void MaxColumnLengthsFor_GivenTwoDataRecordsEachContainingOneField_ReturnsMaxColumnLengthForOneField()
        {
            var dataRecord1 = new DataRecord();
            dataRecord1.Add("field");

            var dataRecord2 = new DataRecord();
            dataRecord2.Add("fieldxxx");

            List<DataRecord> dataRecords = new List<DataRecord> { dataRecord1, dataRecord2 };

            var sut = new MaxConsoleColumnLengthsIdentifier();

            var expectedMaxLengths = new[] { "fieldxxx".Length };
            
            Assert.That(sut.MaxColumnLengthsOf(dataRecords), Is.EqualTo(expectedMaxLengths), "max column lengths");
        }

        [Test]
        public void MaxColumnLengthsFor_GivenTwoDataRecordsEachContainingTwoFields_ReturnsMaxColumnLengthForTwoFields()
        {
            var dataRecord1 = new DataRecord();
            dataRecord1.Add("field_01_xxx");
            dataRecord1.Add("field_02");

            var dataRecord2 = new DataRecord();
            dataRecord2.Add("field_01");
            dataRecord2.Add("field_02_xxx");

            List<DataRecord> dataRecords = new List<DataRecord> { dataRecord1, dataRecord2 };

            var sut = new MaxConsoleColumnLengthsIdentifier();

            var expectedMaxLengths = new[] { "field_01_xxx".Length, "field_02_xxx".Length };
            
            Assert.That(sut.MaxColumnLengthsOf(dataRecords), Is.EqualTo(expectedMaxLengths), "max column lengths");
        }

    }
}
