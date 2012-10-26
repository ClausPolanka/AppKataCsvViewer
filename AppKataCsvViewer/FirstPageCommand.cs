namespace AppKataCsvViewer
{
    public class FirstPageCommand : UserCommand
    {
        private readonly Display display;
        private readonly Browsable browsable;

        public FirstPageCommand(Display display, Browsable browsable)
        {
            this.display = display;
            this.browsable = browsable;
        }
        public void Execute()
        {
            display.Show(browsable.FirstPage());
            display.PrintUserOptionsFor(browsable.PageCount);
        }
    }
}