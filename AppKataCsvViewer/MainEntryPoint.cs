using System;
using System.IO;

namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        private const int FILE_NAME = 0;

        public static void Main(string[] args)
        {
            string csvFileName = args[FILE_NAME];
            int defaultPageSize = 3;

            string[] lines = File.ReadAllLines(csvFileName);

            int lengthColumn1 = 0;
            int lengthColumn2 = 0;
            int lengthColumn3 = 0;

            foreach (string l in lines)
            {
                string[] words = l.Split(';');

                if (lengthColumn1 < words[0].Length)
                    lengthColumn1 = words[0].Length;

                if (lengthColumn2 < words[1].Length)
                    lengthColumn2 = words[1].Length;

                if (lengthColumn3 < words[2].Length)
                    lengthColumn3 = words[2].Length;
            }

            for (int index = 0; index < lines.Length; index++)
            {
                Console.Out.WriteLine(lines[index].Replace(';', '|'));

                if (index == 0)
                {
                    for (int i = 0; i < lines[index].Length; i++)
                        Console.Out.Write("-");

                    Console.Out.WriteLine("");
                }
            }
        }
    }
}