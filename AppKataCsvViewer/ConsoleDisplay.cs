using System;

namespace AppKataCsvViewer
{
    public class ConsoleDisplay : Display
    {
        public const string EXIT_COMMAND = "eX(it";
        public const string ALL_USER_COMMANDS = "N(ext page, P(revious page, F(irst page, L(ast page, eX(it";
        
        public virtual void Show(Page page)
        {
            Console.Out.Write(page.Header());
            Console.Out.Write(page.DataRecords());
        }

        public virtual void PrintUserOptionsFor(int pageCount)
        {
            if (pageCount > 1)
                PrintAllUserOptions();
            else
                PrintExitCommand();
        }

        private void PrintAllUserOptions()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(ALL_USER_COMMANDS);
        }

        private void PrintExitCommand()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(EXIT_COMMAND);
        }
    }
}