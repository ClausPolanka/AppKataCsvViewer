using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        private const string HEADER_SEPARATOR_CHARACTER = "-";
        private const int FILE_NAME = 0;
        private const string SPACE = " ";
        private const string COLUMN_SEPARATOR = "|";
        private const char CSV_SEPARATOR = ';';
        private const string HEADER_COLUMN_SEPARATOR = "+";
        private const int HEADER_ROW = 0;
        
        private static int[] maxColumnLengths;

        public static void Main(string[] args)
        {
            string csvFileName = args[FILE_NAME];

            var csvRows = File.ReadAllLines(csvFileName);

            maxColumnLengths = DetermineMaxColumnLengthsFor(csvRows);

            Show(ToFormattedRows(csvRows));
        }

        private static int[] DetermineMaxColumnLengthsFor(string[] cvsRows)
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

        private static List<List<string>> ToFormattedRows(string[] csvRows)
        {
            var formattedRows = new List<List<string>>();

            foreach (string csvRow in csvRows)
                formattedRows.Add(WithAppliedFormat(csvRow));

            return formattedRows;
        }

        private static List<string> WithAppliedFormat(string csvRow)
        {
            List<string> comaSeparatedValues = csvRow.Split(CSV_SEPARATOR).ToList();

            for (int i = 0; i < comaSeparatedValues.Count; i++)
                comaSeparatedValues[i] = WithAppliedSeparator(comaSeparatedValues[i], i);

            return comaSeparatedValues;
        }

        private static string WithAppliedSeparator(string word, int columnIndex)
        {
            string formattedWord = word;

            for (var i = 0; i < (maxColumnLengths[columnIndex] - word.Length); i++)
                formattedWord += SPACE;

            return formattedWord + COLUMN_SEPARATOR;
        }

        private static void Show(List<List<string>> formattedRows)
        {
            for (int rowIndex = 0; rowIndex < formattedRows.Count; rowIndex++)
            {
                Show(columns: formattedRows[rowIndex]);
                ShowHeaderSeparator(rowIndex);
            }
        }

        private static void Show(List<string> columns)
        {
            foreach (var c in columns)
                Console.Out.Write(c);

            Console.Out.WriteLine();
        }

        private static void ShowHeaderSeparator(int tableRowIndex)
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

        private static void ShowHeaderForColumnWith(int columnLength)
        {
            for (int c = 0; c < columnLength; c++)
                Console.Out.Write(HEADER_SEPARATOR_CHARACTER);
        }
    }
}