using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class PageConsoleFormatter : PageFormatter
    {
        private const string COLUMN_ROW_SEPARATOR = "+";
        private const string ROW_SEPERATOR_CHARACTER = "-";
        private const string COLUMN_SEPARATOR = "|";
        private const string WHITE_SPACE = " ";
        private const string NEW_LINE = "\n";
        private const int INDEX_OF_HEADER = 0;
        private const int INDEX_OF_FIRST_RECORD = 1;

        public string HeaderFor(List<DataRecord> dataRecords)
        {
            string header = HeaderLine(dataRecords);
            header += HeaderLowerBorderLine(dataRecords);
            return header;    
        }

        private string HeaderLine(List<DataRecord> dataRecords)
        {
            string headerLine = String.Empty;
            List<string> headerFields = dataRecords[INDEX_OF_HEADER].Fields;

            for (int i = 0; i < headerFields.Count; i++)
                headerLine += headerFields[i] + WhiteSpacesFor(headerFields[i], MaxColumnLengths(dataRecords)[i]) + COLUMN_SEPARATOR;

            return headerLine;
        }

        private string WhiteSpacesFor(string word, int maxColumnLength)
        {
            if (word.Length == maxColumnLength)
                return String.Empty;

            string spaces = String.Empty;

            for (int i = 0; i < (maxColumnLength - word.Length); i++)
                spaces += WHITE_SPACE;

            return spaces;
        }

        public static int[] MaxColumnLengths(List<DataRecord> dataRecords)
        {
            int columnCount = dataRecords[INDEX_OF_HEADER].ColumnCount;

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

        private string HeaderLowerBorderLine(List<DataRecord> dataRecords)
        {
            string headerLine = NEW_LINE;

            foreach (int maxColumnLength in MaxColumnLengths(dataRecords))
            {
                for (int i = 0; i < maxColumnLength; i++)
                    headerLine += ROW_SEPERATOR_CHARACTER;

                headerLine += COLUMN_ROW_SEPARATOR;
            }

            return headerLine;
        }

        public string Formatted(List<DataRecord> dataRecords)
        {
            string records = NEW_LINE;
            records += Records(dataRecords);
            return records;
        }

        private string Records(List<DataRecord> dataRecords)
        {
            string records = String.Empty;

            for (int recordIndex = INDEX_OF_FIRST_RECORD; recordIndex < dataRecords.Count; recordIndex++)
                records += RecordFor(recordIndex, dataRecords);

            return records;
        }

        private string RecordFor(int recordIndex, List<DataRecord> dataRecords)
        {
            string record = String.Empty;

            for (int wordIndex = 0; wordIndex < dataRecords[recordIndex].ColumnCount; wordIndex++)
            {
                string word = dataRecords[recordIndex].Fields[wordIndex];
                record += word + WhiteSpacesFor(word, MaxColumnLengths(dataRecords)[wordIndex]) + COLUMN_SEPARATOR;
            }

            record += NEW_LINE;

            return record;
        }

    }
}