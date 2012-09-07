using System;
using System.IO;

namespace AppKataCsvViewerSpikes
{
    public class RedirectStandardInSpike
    {
        public static void Main(string[] args)
        {
            Console.Out.WriteLine("Hello");

            TextReader stdin = Console.In;
            Console.SetIn(new StringReader("exit"));

            string line = Console.ReadLine();

            if (line == "exit")
            {
                Console.Out.WriteLine("Juhuuuu");
            }

            Console.SetIn(stdin);

            Console.ReadLine();
        }
    }
}
