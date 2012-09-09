using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class ConsoleDisplay : Display
    {
        private const string ALL_USER_OPTIONS = "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";
        private const string EXIT_COMMAND = "eX(it";
        private const string HEADER_SEPARATOR_CHARACTER = "-";
        private const string HEADER_COLUMN_SEPARATOR = "+";
        private const int HEADER = 1;

        public void Show(CsvTable csvTable)
        {
            for (int rowIndex = 0; rowIndex < (csvTable.PageSize + HEADER); rowIndex++)
            {
                Show(csvTable.FormattedRows[rowIndex]);
                ShowHeaderSeparator(rowIndex, csvTable.MaxColumnLengths);
            }

            if (AllUserOptionsMustBeShown(csvTable.FormattedRows, csvTable.PageSize))
            {
                ShowAllUserOptions();
            }
            else
                ShowExitCommand();
        }

        private bool AllUserOptionsMustBeShown(List<List<string>> formattedRows, int pageSize)
        {
            return (formattedRows.Count - HEADER) > pageSize;
        }

        private void Show(List<string> columns)
        {
            foreach (var c in columns)
                Console.Out.Write(c);

            Console.Out.WriteLine();
        }

        private void ShowHeaderSeparator(int tableRowIndex, int[] maxColumnLengths)
        {
            if (tableRowIndex != 0)
                return;

            for (int columnIndex = 0; columnIndex < maxColumnLengths.Length; columnIndex++)
            {
                ShowHeaderForColumnWith(maxColumnLengths[columnIndex]);
                Console.Out.Write(HEADER_COLUMN_SEPARATOR);
            }

            Console.Out.WriteLine();
        }

        private void ShowHeaderForColumnWith(int columnLength)
        {
            for (int c = 0; c < columnLength; c++)
                Console.Out.Write(HEADER_SEPARATOR_CHARACTER);
        }

        private void ShowAllUserOptions()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(ALL_USER_OPTIONS);
        }

        private void ShowExitCommand()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(EXIT_COMMAND);
        }
    }
}