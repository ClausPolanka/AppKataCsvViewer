using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class Page
    {
        private const int INDEX_OF_HEADER = 0;
        private const int INDEX_OF_FIRST_RECORD = 1;
        private const string COLUMN_SEPARATOR = "|";
        private const string COLUMN_ROW_SEPARATOR = "+";
        private const string ROW_SEPERATOR_CHARACTER = "-";
        private const string WHITE_SPACE = " ";
        private const string NEW_LINE = "\n";

        private readonly List<DataRecord> dataRecords = new List<DataRecord>();

        public void Add(DataRecord dataRecord)
        {
            dataRecords.Add(dataRecord);
        }

        public string Header()
        {
            string header = HeaderLine();
            header += HeaderFooterLine();
            return header;
        }

        private string HeaderLine()
        {
            string headerLine = String.Empty;
            List<string> headerFields = HeaderFields();

            for (int i = 0; i < headerFields.Count; i++)
                headerLine += headerFields[i] + WhiteSpacesFor(headerFields[i], MaxColumnLengths()[i]) + COLUMN_SEPARATOR;

            return headerLine;
        }

        private List<string> HeaderFields()
        {
            return DataRecords[INDEX_OF_HEADER].Fields;
        }

        public int[] MaxColumnLengths()
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

        private string WhiteSpacesFor(string word, int maxColumnLength)
        {
            if (word.Length == maxColumnLength)
                return String.Empty;

            string spaces = String.Empty;

            for (int i = 0; i < (maxColumnLength - word.Length); i++)
                spaces += WHITE_SPACE;

            return spaces;
        }

        private string HeaderFooterLine()
        {
            string headerLine = NEW_LINE;

            foreach (int maxColumnLength in MaxColumnLengths())
            {
                for (int i = 0; i < maxColumnLength; i++)
                    headerLine += ROW_SEPERATOR_CHARACTER;

                headerLine += COLUMN_ROW_SEPARATOR;
            }

            return headerLine;
        }

        public string GetDataRecords()
        {
            string records = NEW_LINE;
            records += Records();
            return records;
        }

        private string Records()
        {
            string records = String.Empty;

            List<DataRecord> dataRecords = DataRecords;

            for (int recordIndex = INDEX_OF_FIRST_RECORD; recordIndex < dataRecords.Count; recordIndex++)
                records += PrintDataRecord(recordIndex, dataRecords);

            return records;
        }

        private string PrintDataRecord(int recordIndex, List<DataRecord> dataRecords)
        {
            string record = String.Empty;

            for (int wordIndex = 0; wordIndex < dataRecords[recordIndex].ColumnCount; wordIndex++)
            {
                string word = dataRecords[recordIndex].Fields[wordIndex];
                record += word + WhiteSpacesFor(word, MaxColumnLengths()[wordIndex]) + COLUMN_SEPARATOR;
            }

            record += NEW_LINE;

            return record;
        }

        public List<DataRecord> DataRecords { get { return dataRecords; } }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString() && GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            string page = String.Empty;

            foreach (var r in dataRecords)
                page += r + WHITE_SPACE;

            return page;
        }
    }
}