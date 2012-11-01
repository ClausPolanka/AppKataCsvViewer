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
            ExecuteEnteredUserCommand();
        }

        private void ExecuteEnteredUserCommand()
        {
            string command;
            
            while ( ! string.IsNullOrEmpty(command = userCommandReceiver.ReceiveUserCommand()))
            {
                if (IsExitCommand(command))
                    break;

                commands.Execute(command);
            }

            Validate(command);
        }

        private bool IsExitCommand(string command)
        {
            return command.ToLower() == "x" || command.ToLower() == "exit";
        }

        private void Validate(string command)
        {
            if (IsNotValid(command))
                throw new InvalidUserCommand(command);
        }

        private bool IsNotValid(string command)
        {
            return command == null || (! IsExitCommand(command));
        }

        public class InvalidUserCommand : Exception
        {
            public InvalidUserCommand(string command) : base(command)
            {
            }
        }
    }
}