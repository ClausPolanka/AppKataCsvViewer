using System;
using System.Collections.Generic;
using System.Linq;

namespace AppKataCsvViewer
{
    public class ConsoleDisplay : Display
    {
        private const int INDEX_OF_HEADER = 0;
        private const int INDEX_OF_FIRST_RECORD = 1;

        private const string EXIT_COMMAND = "eX(it";
        private const string ALL_USER_OPTIONS = "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

        private const string WHITE_SPACE = " ";
        private const string COLUMN_SEPARATOR = "|";
        private const string COLUMN_ROW_SEPARATOR = "+";
        private const string ROW_SEPERATOR_CHARACTER = "-";

        private int pageNumber;

        public void Show(Table table)
        {
            Page page = table.Pages[pageNumber++];

            PrintHeaderFor(page);
            PrintDataRecordsFor(page);
            PrintUserOptionsFor(table.Pages);
        }

        private void PrintHeaderFor(Page page)
        {
            List<string> headerColumns = page.DataRecords[INDEX_OF_HEADER].Words;

            for (int i = 0; i < headerColumns.Count; i++)
                Console.Out.WriteLine(
                    headerColumns[i] + WhiteSpacesFor(headerColumns[i], page.MaxColumnLengths()[i]) + COLUMN_SEPARATOR);

            PrintHeaderLine(page.MaxColumnLengths());
        }

        private string WhiteSpacesFor(string word, int maxColumnLength)
        {
            if (word.Length == maxColumnLength)
                return string.Empty;

            string spaces = string.Empty;

            for (int i = 0; i < (maxColumnLength - word.Length); i++)
                spaces += WHITE_SPACE;

            return spaces;
        }

        private void PrintHeaderLine(IEnumerable<int> maxColumnLengths)
        {
            foreach (int maxColumnLength in maxColumnLengths)
            {
                for (int i = 0; i < maxColumnLength; i++)
                    Console.Out.Write(ROW_SEPERATOR_CHARACTER);

                Console.Out.Write(COLUMN_ROW_SEPARATOR);
            }
        }

        private void PrintDataRecordsFor(Page page)
        {
            List<DataRecord> dataRecords = page.DataRecords;

            for (int recordIndex = INDEX_OF_FIRST_RECORD; recordIndex < dataRecords.Count; recordIndex++)
            {
                for (int wordIndex = 0; wordIndex < dataRecords[recordIndex].ColumnCount; wordIndex++)
                {
                    string word = dataRecords[recordIndex].Words[wordIndex];
                    Console.Out.Write(word + WhiteSpacesFor(word, page.MaxColumnLengths()[wordIndex]) + COLUMN_SEPARATOR);
                }
            }
        }

        private void PrintUserOptionsFor(List<Page> pages)
        {
            if (pages.Count > 1)
                PrintAllUserOptions();
            else
                PrintExitCommand();
        }

        private void PrintAllUserOptions()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(ALL_USER_OPTIONS);
        }

        private void PrintExitCommand()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(EXIT_COMMAND);
        }
    }
}