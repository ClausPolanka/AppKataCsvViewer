using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class MaxColumnLengthsIdentifier
    {
        private const int HEADER_INDEX = 0;

        public int[] MaxColumnLengthsOf(List<DataRecord> dataRecords)
        {
            int columnCount = dataRecords[HEADER_INDEX].ColumnCount;

            int[] maxLengths = new int[columnCount];

            foreach (DataRecord record in dataRecords)
            {
                for (int columnIndex = 0; columnIndex < record.ColumnCount; columnIndex++)
                {
                    if (maxLengths[columnIndex] < record.Fields[columnIndex].Length)
                        maxLengths[columnIndex] = record.Fields[columnIndex].Length;
                }
            }

            return maxLengths;
        }
    }
}