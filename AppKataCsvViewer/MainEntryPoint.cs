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
            string[] lines = File.ReadAllLines(csvFileName);
            
            foreach (var line in lines)
                Console.Out.WriteLine(line);
        }
    }
}