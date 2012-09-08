namespace AppKataCsvViewer
{
    public class MainEntryPoint
    {
        private const int FILE_NAME = 0;

        public static void Main(string[] args)
        {
            new CsvViewer().Show(args[FILE_NAME]);
        }
    }
}