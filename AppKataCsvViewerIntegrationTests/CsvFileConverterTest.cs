using System.Collections.Generic;
using System.IO;
using AppKataCsvViewer;
using NUnit.Framework;
using System.Linq;

namespace AppKataCsvViewerIntegrationTests
{
    [TestFixture]
    public class CsvFileConverterTest
    {
        private const string CSV_FILE_NAME = "persons.csv";

        [Test]
        public void ToDataRecords_GivenOneLineOfCsvContent_GenerateOneDataRecord()
        {
            var csvContent = new[] { "Field1;Field2;Field3" };

            CreateTemporaryCsvFileWith(csvContent);

            var sut = new CsvFileConverter();
            List<DataRecord> actualRecords = sut.ToDataRecords(CSV_FILE_NAME);

            List<DataRecord> expectedRecords = CreateExpectedRecords(howMany: 1);

            Assert.That(actualRecords, Is.EqualTo(expectedRecords), "data records");
        }

        [Test]
        public void ToDataRecords_GivenTwoLinesOfCsvContent_GenerateTwoDataRecord()
        {
            var csvContent = new[] { "Field1;Field2;Field3", "Field1;Field2;Field3" };

            CreateTemporaryCsvFileWith(csvContent);

            var sut = new CsvFileConverter();
            List<DataRecord> actualRecords = sut.ToDataRecords(CSV_FILE_NAME);

            List<DataRecord> expectedRecords = CreateExpectedRecords(howMany: 2);

            Assert.That(actualRecords, Is.EqualTo(expectedRecords), "data records");
        }

        [Test]
        public void ToDataRecords_GivenNoCsvContent_GenerateEmptyRecord()
        {
            var csvContent = new string[0];

            CreateTemporaryCsvFileWith(csvContent);

            var sut = new CsvFileConverter();
            List<DataRecord> actualRecords = sut.ToDataRecords(CSV_FILE_NAME);

            Assert.That(actualRecords.Any(), Is.False, "data records has no elements");
        }

        [Test]
        public void ToDataRecords_GivenAnEmptyStringAsCsvContent_GenerateEmptyRecords()
        {
            var csvContent = new [] { string.Empty };

            CreateTemporaryCsvFileWith(csvContent);

            var sut = new CsvFileConverter();
            List<DataRecord> actualRecords = sut.ToDataRecords(CSV_FILE_NAME);

            Assert.That(actualRecords.Any(), Is.False, "data records has no elements");
        }

        private List<DataRecord> CreateExpectedRecords(int howMany)
        {
            List<DataRecord> expectedRecrods = new List<DataRecord>();
            
            for (int i = 0; i < howMany; i++)
            {
                var dataRecord = new DataRecord();
                dataRecord.Add("Field1");
                dataRecord.Add("Field2");
                dataRecord.Add("Field3");
                expectedRecrods.Add(dataRecord);
            }
            
            return expectedRecrods;
        }

        private void CreateTemporaryCsvFileWith(string[] csvContent)
        {
            File.WriteAllLines(CSV_FILE_NAME, csvContent);
        }

        [TearDown]
        public virtual void TearDown()
        {
            File.Delete(CSV_FILE_NAME);
        }
    }
}