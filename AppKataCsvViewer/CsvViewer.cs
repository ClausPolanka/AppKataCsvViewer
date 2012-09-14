using System;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private readonly Display display;
        private readonly CommandReader commandReader;

        public CsvViewer(CommandReader commandReader, Display display)
        {
            this.commandReader = commandReader;
            this.display = display;
        }

        public void Show(Table table)
        {
            display.Show(table);
            ExecuteCommandEnteredByUser(table);
        }

        private void ExecuteCommandEnteredByUser(Table table)
        {
            string command;

            while ( ! string.IsNullOrEmpty(command = commandReader.ReadCommand()))
            {
                if (command.ToLower() == "x" || command.ToLower() == "exit")
                {
                    break;
                }
                if (command.ToLower() == "n" || command.ToLower() == "next")
                {
                    display.Show(table);
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