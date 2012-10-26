namespace AppKataCsvViewer
{
    public class LastPageCommand : UserCommand
    {
        private readonly Display display;
        private readonly Browsable browsable;

        public LastPageCommand(Display display, Browsable browsable)
        {
            this.display = display;
            this.browsable = browsable;
        }

        public void Execute()
        {
            display.Show(browsable.LastPage());
            display.PrintUserOptionsFor(browsable.PageCount);
        }
    }
}