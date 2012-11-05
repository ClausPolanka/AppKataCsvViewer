using System;
using System.IO;
using System.Text;
using System.Threading;
using AppKataCsvViewer;

namespace AppKataCsvViewerEndToEndTests
{
    public class ApplicationTestExecutor : UserCommandReceiverListener
    {
        private StringWriter consoleOutput;
        private Thread thread;

        public ApplicationTestExecutor(StringWriter consoleOutput)
        {
            this.consoleOutput = consoleOutput;
        }

        public virtual void ExecuteViewerFor(string csvFileName)
        {
            MainEntryPoint.user = this;
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
            sb.Clear();
        }

        public virtual void ReadsUserCommmand(string cmd)
        {
            if (cmd.ToLower() == "n" || cmd.ToLower() == "next")
                ClearConsoleOutputBuffer();

            if (cmd.ToLower() == "p" || cmd.ToLower() == "previous")
                ClearConsoleOutputBuffer();
            
            if (cmd.ToLower() == "l" || cmd.ToLower() == "last")
                ClearConsoleOutputBuffer();

            if (cmd.ToLower() == "f" || cmd.ToLower() == "first")
                ClearConsoleOutputBuffer();

            Console.SetIn(new StringReader(cmd));
            thread.Join(200);
        }

        public virtual void NotifyNewCommand()
        {
            Thread.Sleep(200);
        }
    }
}