using System.Collections.Generic;
using System.IO;

namespace AppKataCsvViewer
{
    public class FileCsvContentLoader
    {
        private const char LINE_SEPARATOR = ';';

        public List<DataRecord> LoadDataRecords(string csvFileName)
        {
            List<DataRecord> records = new List<DataRecord>();

            foreach (string cvsLine in File.ReadAllLines(csvFileName))
            {
                var record = new DataRecord();

                foreach (var word in cvsLine.Split(LINE_SEPARATOR))
                    record.Add(word);

                records.Add(record);
            }

            return records;
        }
    }
}