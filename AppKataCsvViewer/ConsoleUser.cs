using System;

namespace AppKataCsvViewer
{
    public class ConsoleUser : User
    {
        private readonly UserCommandReceiverListener user;
        private readonly CsvUserCommands userCommands;

        public ConsoleUser(UserCommandReceiverListener user, CsvUserCommands userCommands)
        {
            this.user = user;
            this.userCommands = userCommands;
        }

        public virtual UserCommand EnteredCommand()
        {
            user.NotifyNewCommand();
            return userCommands.CreateCommand(Console.ReadLine());
        }
    }
}