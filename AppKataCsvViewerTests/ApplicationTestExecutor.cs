using AppKataCsvViewer;

namespace AppKataCsvViewerTests
{
    public class ApplicationTestExecutor
    {
        public void ExecuteViewerFor(string csvFileName)
        {
            MainEntryPoint.Main(new[] { csvFileName });
        }
    }
}