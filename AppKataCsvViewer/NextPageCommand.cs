namespace AppKataCsvViewer
{
    public class NextPageCommand : UserCommand
    {
        private readonly Display display;
        private readonly Browsable browsable;

        public NextPageCommand(Display display, Browsable browsable)
        {
            this.display = display;
            this.browsable = browsable;
        }

        public void Execute()
        {
            display.Show(browsable.NextPage());
            display.PrintUserOptionsFor(browsable.PageCount);
        }
    }
}