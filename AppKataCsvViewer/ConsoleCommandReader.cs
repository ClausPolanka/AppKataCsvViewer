using System;

namespace AppKataCsvViewer
{
    internal class ConsoleCommandReader : CommandReader
    {
        private CommandReaderListener commandReaderListener;

        public ConsoleCommandReader(CommandReaderListener commandReaderListener)
        {
            this.commandReaderListener = commandReaderListener;
        }

        public string ReadCommand()
        {
            commandReaderListener.NotifyNewCommand();
            return Console.ReadLine();
        }
    }
}