using System;

namespace AppKataCsvViewerSpikes
{
    public class ReadFromStandardInSpike
    {
        public static void Main(string[] args)
        {
            Console.Write("> ");
            
            string line;

            while ( ! string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                if (line.ToLower() == "exit" || line.ToLower() == "x")
                {
                    break;
                }

                Console.Out.WriteLine(line);
                Console.Write("> ");
            }

            Console.ReadLine();
        }
    }
}
