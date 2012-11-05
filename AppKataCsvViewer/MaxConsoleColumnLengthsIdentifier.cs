using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class MaxConsoleColumnLengthsIdentifier : MaxConsoleLengthsIdentifier
    {
        private const int ANY_RECORD = 0;

        public virtual int[] MaxColumnLengthsOf(List<DataRecord> dataRecords)
        {
            int columnCount = dataRecords[ANY_RECORD].ColumnCount;

            int[] maxLengths = new int[columnCount];

            foreach (DataRecord record in dataRecords)
                CalculateMaximum(maxLengths, record);

            return maxLengths;
        }

        private void CalculateMaximum(int[] maxLengths, DataRecord record)
        {
            for (int col = 0; col < record.ColumnCount; col++)
            {
                if (maxLengths[col] < record.Fields[col].Length)
                    maxLengths[col] = record.Fields[col].Length;
            }
        }
    }
}