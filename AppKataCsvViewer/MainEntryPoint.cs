using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        // Public for testing purposes.
        public static UserCommandReceiverListener userCommandReceiverListener = new UserCommandReceiverListenerNullObject();
        
        private const int FILE_NAME = 0;

        public static void Main(string[] args)
        {
            List<DataRecord> dataRecords = new CsvFileConverter().ToDataRecords(args[FILE_NAME]);
            var csvUserCommands = new CsvUserCommands(new ConsoleDisplay(), new Table(dataRecords, 3));
            var csvViewer = new CsvViewer(new ConsoleUserUserCommandReceiver(userCommandReceiverListener), csvUserCommands);
            csvViewer.Show();
        }
    }

    public class UserCommandReceiverListenerNullObject : UserCommandReceiverListener
    {
        public void NotifyNewCommand()
        {
        }
    }
}