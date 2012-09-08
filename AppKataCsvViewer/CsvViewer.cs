using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private const string HEADER_SEPARATOR_CHARACTER = "-";
        private const string SPACE = " ";
        private const string COLUMN_SEPARATOR = "|";
        private const char CSV_SEPARATOR = ';';
        private const string HEADER_COLUMN_SEPARATOR = "+";
        private const int HEADER_ROW = 0;
        private const string EXIT_COMMAND = "eX(it";

        private int[] maxColumnLengths;

        private readonly CommandReader commandReader;

        public CsvViewer(CommandReader commandReader)
        {
            this.commandReader = commandReader;
        }

        public void Show(string csvFileName)
        {
            var csvRows = File.ReadAllLines(csvFileName);

            maxColumnLengths = DetermineMaxColumnLengthsFor(csvRows);

            Show(ToFormattedRows(csvRows));
            ExecuteCommandEnteredByUser();
        }
        private int[] DetermineMaxColumnLengthsFor(string[] cvsRows)
        {
            int numberOfColumns = cvsRows[HEADER_ROW].Split(CSV_SEPARATOR).Length;

            int[] maxColumnLengths = new int[numberOfColumns];

            foreach (string cvsRow in cvsRows)
            {
                string[] words = cvsRow.Split(CSV_SEPARATOR);

                for (int i = 0; i < words.Length; i++)
                {
                    if (maxColumnLengths[i] < words[i].Length)
                    {
                        maxColumnLengths[i] = words[i].Length;
                    }
                }
            }

            return maxColumnLengths;
        }

        private List<List<string>> ToFormattedRows(string[] csvRows)
        {
            var formattedRows = new List<List<string>>();

            foreach (string csvRow in csvRows)
                formattedRows.Add(WithAppliedFormat(csvRow));

            return formattedRows;
        }

        private List<string> WithAppliedFormat(string csvRow)
        {
            List<string> comaSeparatedValues = csvRow.Split(CSV_SEPARATOR).ToList();

            for (int i = 0; i < comaSeparatedValues.Count; i++)
                comaSeparatedValues[i] = WithAppliedSeparator(comaSeparatedValues[i], i);

            return comaSeparatedValues;
        }

        private string WithAppliedSeparator(string word, int columnIndex)
        {
            string formattedWord = word;

            for (var i = 0; i < (maxColumnLengths[columnIndex] - word.Length); i++)
                formattedWord += SPACE;

            return formattedWord + COLUMN_SEPARATOR;
        }

        private void Show(List<List<string>> formattedRows)
        {
            for (int rowIndex = 0; rowIndex < formattedRows.Count; rowIndex++)
            {
                Show(columns: formattedRows[rowIndex]);
                ShowHeaderSeparator(rowIndex);
            }

            ShowUserOptions();
        }

        private void Show(List<string> columns)
        {
            foreach (var c in columns)
                Console.Out.Write(c);

            Console.Out.WriteLine();
        }

        private void ShowHeaderSeparator(int tableRowIndex)
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

        private void ShowUserOptions()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(EXIT_COMMAND);
        }

        private void ExecuteCommandEnteredByUser()
        {
            string command;

            while (!string.IsNullOrEmpty(command = commandReader.ReadCommand()))
            {
                if (command.ToLower() == "x" || command.ToLower() == "exit")
                {
                    break;
                }

                // TODO: Execute entered command.
            }

            if (command == null || command.ToLower() != "x" && command.ToLower() != "exit")
            {
                throw new Exception("No command was entered by user");
            }
        }
    }
}