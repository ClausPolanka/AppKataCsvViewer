using AppKataCsvViewer;

namespace AppKataCsvViewerTests
{
    public class ApplicationTestExecutor
    {
        private readonly CommandReaderListener testCommandReaderListener;

        public ApplicationTestExecutor(CommandReaderListener testCommandReaderListener)
        {
            this.testCommandReaderListener = testCommandReaderListener;
        }

        public void ExecuteViewerFor(string csvFileName)
        {
            MainEntryPoint.commandReaderListener = testCommandReaderListener;
            MainEntryPoint.Main(new[] { csvFileName });
        }
    }
}