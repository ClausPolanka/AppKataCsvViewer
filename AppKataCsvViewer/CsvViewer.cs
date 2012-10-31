using System;

namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private readonly UserCommandReceiver userCommandReceiver;
        private UserCommands commands;

        public CsvViewer(UserCommandReceiver userCommandReceiver, UserCommands commands)
        {
            this.userCommandReceiver = userCommandReceiver;
            this.commands = commands;
        }

        public void Execute()
        {
            commands.FirstPage();
            ExecuteCommandEnteredByUser();
        }

        private void ExecuteCommandEnteredByUser()
        {
            string command;
            
            while ( ! string.IsNullOrEmpty(command = userCommandReceiver.ReceiveUserCommand()))
            {
                if (command.ToLower() == "x" || command.ToLower() == "exit")
                    break;

                commands.Execute(command);
            }

            if (command == null || command.ToLower() != "x" && command.ToLower() != "exit")
                throw new Exception("No or wrong command was entered by user: " + command);
        }
    }
}