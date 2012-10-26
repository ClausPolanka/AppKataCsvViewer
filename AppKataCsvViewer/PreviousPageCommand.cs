namespace AppKataCsvViewer
{
    public class PreviousPageCommand : UserCommand
    {
        private readonly Display display;
        private readonly Browsable browsable;

        public PreviousPageCommand(Display display, Browsable browsable)
        {
            this.display = display;
            this.browsable = browsable;
        }

        public void Execute()
        {
            display.Show(browsable.PreviousPage());
            display.PrintUserOptionsFor(browsable.PageCount);            
        }
    }
}