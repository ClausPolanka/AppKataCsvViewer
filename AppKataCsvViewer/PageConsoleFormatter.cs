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
        private const int HEADER_INDEX = 0;
        private const int INDEX_OF_FIRST_RECORD = 1;
        
        private readonly MaxConsoleLengthsIdentifier maxConsoleColumnLengthsIdentifier;

        public PageConsoleFormatter(MaxConsoleLengthsIdentifier maxConsoleColumnLengthsIdentifier)
        {
            this.maxConsoleColumnLengthsIdentifier = maxConsoleColumnLengthsIdentifier;
        }

        public virtual string HeaderFor(List<DataRecord> dataRecords)
        {
            string header = HeaderLine(dataRecords);
            header += HeaderLowerBorderLine(dataRecords);
            return header;    
        }

        private string HeaderLine(List<DataRecord> dataRecords)
        {
            string headerLine = String.Empty;
            List<string> headerFields = dataRecords[HEADER_INDEX].Fields;

            for (int i = 0; i < headerFields.Count; i++)
                headerLine += headerFields[i] + WhiteSpacesFor(headerFields[i], MaxColumnLengths(dataRecords)[i]) + COLUMN_SEPARATOR;

            return headerLine;
        }

        private string WhiteSpacesFor(string field, int maxColumnLength)
        {
            if (field.Length == maxColumnLength)
                return String.Empty;

            string spaces = String.Empty;

            for (int i = 0; i < (maxColumnLength - field.Length); i++)
                spaces += WHITE_SPACE;

            return spaces;
        }

        private int[] MaxColumnLengths(List<DataRecord> dataRecords)
        {
            return maxConsoleColumnLengthsIdentifier.MaxColumnLengthsOf(dataRecords);
        }

        private string HeaderLowerBorderLine(List<DataRecord> dataRecords)
        {
            string headerLine = NEW_LINE;

            foreach (int max in MaxColumnLengths(dataRecords))
            {
                for (int i = 0; i < max; i++)
                    headerLine += ROW_SEPERATOR_CHARACTER;

                headerLine += COLUMN_ROW_SEPARATOR;
            }

            return headerLine;
        }

        public virtual string Formatted(List<DataRecord> dataRecords)
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

            for (int fieldIndex = 0; fieldIndex < dataRecords[recordIndex].ColumnCount; fieldIndex++)
            {
                string field = dataRecords[recordIndex].Fields[fieldIndex];
                record += field + WhiteSpacesFor(field, MaxColumnLengths(dataRecords)[fieldIndex]) + COLUMN_SEPARATOR;
            }

            record += NEW_LINE;

            return record;
        }

    }
}