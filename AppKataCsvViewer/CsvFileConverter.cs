using System.Collections.Generic;
using System.IO;

namespace AppKataCsvViewer
{
    public class CsvFileConverter
    {
        private const char LINE_SEPARATOR = ';';

        public virtual List<DataRecord> ToDataRecords(string csvFileName)
        {
            List<DataRecord> records = new List<DataRecord>();

            foreach (string csvLine in File.ReadAllLines(csvFileName))
            {
                if (string.IsNullOrEmpty(csvLine))
                    break;

                records.Add(CreateRecordFor(csvLine));
            }

            return records;
        }

        private DataRecord CreateRecordFor(string csvLine)
        {
            var record = new DataRecord();

            foreach (var field in csvLine.Split(LINE_SEPARATOR))
                record.Add(field);

            return record;
        }
    }
}