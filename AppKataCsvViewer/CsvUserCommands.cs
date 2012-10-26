using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class CsvUserCommands
    {
        public const string NEXT = "next";

        private readonly Dictionary<string, UserCommand> commands = new Dictionary<string, UserCommand>();

        public CsvUserCommands(Display display, Browsable table)
        {
            commands.Add("n", new NextPageCommand(display, table));
            commands.Add(NEXT, new NextPageCommand(display, table));
            commands.Add("p", new PreviousPageCommand(display, table));
            commands.Add("previous", new PreviousPageCommand(display, table));
            commands.Add("f", new FirstPageCommand(display, table));
            commands.Add("first", new FirstPageCommand(display, table));
            commands.Add("l", new LastPageCommand(display, table));
            commands.Add("last", new LastPageCommand(display, table));
        }

        public void Execute(string command)
        {
            commands[command.ToLower()].Execute();
        }
    }
}