using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class Page
    {
        private const int INDEX_OF_HEADER = 0;

        private readonly List<DataRecord> dataRecords = new List<DataRecord>();

        public void Add(DataRecord dataRecord)
        {
            dataRecords.Add(dataRecord);
        }

        public int[] MaxColumnLengths()
        {
            int columnCount = dataRecords[INDEX_OF_HEADER].ColumnCount;

            int[] maxLengths = new int[columnCount];

            foreach (DataRecord record in dataRecords)
            {
                for (int columnIndex = 0; columnIndex < record.ColumnCount; columnIndex++)
                {
                    if (maxLengths[columnIndex] < record.Words[columnIndex].Length)
                        maxLengths[columnIndex] = record.Words[columnIndex].Length;
                }
            }

            return maxLengths;
        }

        public List<DataRecord> DataRecords { get { return dataRecords; } }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            string page = string.Empty;

            foreach (var r in dataRecords)
                page += r + " ";

            return page;
        }
    }
}