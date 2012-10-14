using System;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private readonly Display display;
        private readonly UserCommandReceiver userCommandReceiver;

        public CsvViewer(UserCommandReceiver userCommandReceiver, Display display)
        {
            this.userCommandReceiver = userCommandReceiver;
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

            while ( ! string.IsNullOrEmpty(command = userCommandReceiver.ReceiveUserCommand()))
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