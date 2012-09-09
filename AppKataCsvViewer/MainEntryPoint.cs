namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        // Public for testing purposes.
        public static CommandReaderListener commandReaderListener = new CommandReaderListenerNullObject();
        
        private const int FILE_NAME = 0;

        public static void Main(string[] args)
        {
            var csvViewer = new CsvViewer(new ConsoleCommandReader(commandReaderListener), new ConsoleDisplay());
            csvViewer.Show(args[FILE_NAME]);
        }
    }

    public class CommandReaderListenerNullObject : CommandReaderListener
    {
        public void NotifyNewCommand()
        {
        }
    }
}