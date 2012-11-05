namespace AppKataCsvViewer
{
    public class CsvViewer
    {
        private readonly User user;
        private readonly UserCommands commands;

        public CsvViewer(User user, UserCommands commands)
        {
            this.user = user;
            this.commands = commands;
        }

        public virtual void Execute()
        {
            commands.FirstPage();
            ExecuteEnteredUserCommand();
        }

        private void ExecuteEnteredUserCommand()
        {
            UserCommand command;

            while ((command = user.EnteredCommand()).IsNotExitCommand())
            {
                commands.Execute(command);
            }
        }
    }
}