using System;
using System.IO;
using System.Text;
using System.Threading;
using AppKataCsvViewer;

namespace AppKataCsvViewerEndToEndTests
{
    public class ApplicationTestExecutor : CommandReaderListener
    {
        private readonly StringWriter consoleOutput;
        private Thread thread;

        public ApplicationTestExecutor(StringWriter consoleOutput)
        {
            this.consoleOutput = consoleOutput;
        }

        public void ExecuteViewerFor(string csvFileName)
        {
            MainEntryPoint.commandReaderListener = this;
            thread = new Thread(ExecuteCsvViewer);
            thread.Start(csvFileName);
            thread.Join(100);
        }

        private void ExecuteCsvViewer(object fileName)
        {
            try
            {
                MainEntryPoint.Main(new[] { Convert.ToString(fileName) });
            }
            catch (Exception e)
            {
                ClearConsoleOutputBuffer();
                Console.Out.Write(e.Message);
            }
        }

        private void ClearConsoleOutputBuffer()
        {
            StringBuilder sb = consoleOutput.GetStringBuilder();
            sb.Remove(0, sb.Length - 1);
        }

        public void ReadsUserCommmand(string cmd)
        {
            if (cmd.ToLower() == "n" || cmd.ToLower() == "next")
            {
                ClearConsoleOutputBuffer();
            }
            Console.SetIn(new StringReader(cmd));
            thread.Join(200);
        }

        public void NotifyNewCommand()
        {
            Thread.Sleep(200);
        }
    }
}