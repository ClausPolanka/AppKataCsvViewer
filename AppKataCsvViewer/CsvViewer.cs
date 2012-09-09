using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private const string SPACE = " ";
        private const string COLUMN_SEPARATOR = "|";
        private const char CSV_SEPARATOR = ';';
        private const int HEADER_ROW = 0;

        private readonly Display display;
        private readonly CommandReader commandReader;

        private int[] maxColumnLengths;

        public CsvViewer(CommandReader commandReader, Display display)
        {
            this.commandReader = commandReader;
            this.display = display;
        }

        public void Show(string csvFileName, int pageSize = 3)
        {
            string[] csvRows = File.ReadAllLines(csvFileName); // TODO: Encapsulate io.

            maxColumnLengths = DetermineMaxColumnLengthsFor(csvRows, pageSize);

            display.Show(new CsvTable(ToFormattedRows(csvRows), pageSize, maxColumnLengths));
            ExecuteCommandEnteredByUser();
        }

        private int[] DetermineMaxColumnLengthsFor(string[] cvsRows, int pageSize)
        {
            int numberOfColumns = cvsRows[HEADER_ROW].Split(CSV_SEPARATOR).Length;

            int[] maxColumnLengths = new int[numberOfColumns];

            for (int colIndex = 0; colIndex < pageSize; colIndex++)
            {
                string cvsRow = cvsRows[colIndex];
                string[] words = cvsRow.Split(CSV_SEPARATOR);

                for (int i = 0; i < words.Length; i++)
                {
                    if (maxColumnLengths[i] < words[i].Length)
                        maxColumnLengths[i] = words[i].Length;
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

        private void ExecuteCommandEnteredByUser()
        {
            string command;

            while ( ! string.IsNullOrEmpty(command = commandReader.ReadCommand()))
            {
                if (command.ToLower() == "x" || command.ToLower() == "exit")
                {
                    break;
                }

                // TODO: Execute entered command.
            }
            if (command == null || command.ToLower() != "x" && command.ToLower() != "exit")
            {
                throw new Exception("No or wrong command was entered by user: " + command);
            }
        }
    }
}