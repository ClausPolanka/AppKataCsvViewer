using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class ConsoleDisplay : Display
    {
        private const string EXIT_COMMAND = "eX(it";
        private const string ALL_USER_OPTIONS = "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";

        private int pageNumber;

        public void Show(Table table)
        {
            Page page = table.Pages[pageNumber++];

            Console.Out.Write(page.Header());
            Console.Out.Write(page.GetDataRecords());
            
            PrintUserOptionsFor(table.Pages);
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