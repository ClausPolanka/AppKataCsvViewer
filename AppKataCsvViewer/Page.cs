using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class Page
    {
        private const int INDEX_OF_HEADER = 0;
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

        public string Header()
        {
            string header = HeaderLine();
            header += HeaderFooterLine();
            return header;
        }

        private string HeaderLine()
        {
            string headerLine = string.Empty;
            List<string> headerFields = HeaderFields();

            for (int i = 0; i < headerFields.Count; i++)
                headerLine += headerFields[i] + ConsoleDisplay.WhiteSpacesFor(headerFields[i], MaxColumnLengths()[i]) + COLUMN_SEPARATOR;

            return headerLine;
        }

        private List<string> HeaderFields()
        {
            return DataRecords[INDEX_OF_HEADER].Words;
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