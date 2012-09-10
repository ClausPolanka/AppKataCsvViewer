using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        // Public for testing purposes.
        public static CommandReaderListener commandReaderListener = new CommandReaderListenerNullObject();
        
        private const int FILE_NAME = 0;

        public static void Main(string[] args)
        {
            List<DataRecord> dataRecords = new FileCsvContentLoader().LoadDataRecords(args[FILE_NAME]);

            var table = new Table(dataRecords, pageSize: 3);

            var csvViewer = new CsvViewer(new ConsoleCommandReader(commandReaderListener), new ConsoleDisplay());
            
            csvViewer.Show(table);
        }
    }

    public class CommandReaderListenerNullObject : CommandReaderListener
    {
        public void NotifyNewCommand()
        {
        }
    }
}