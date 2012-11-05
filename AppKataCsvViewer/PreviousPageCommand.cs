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

        public virtual void Execute()
        {
            display.Show(browsable.PreviousPage());
            display.PrintUserOptionsFor(browsable.PageCount);            
        }

        public virtual bool IsNotExitCommand()
        {
            return true;
        }

        public virtual bool Equals(PreviousPageCommand other)
        {
            if (ReferenceEquals(null, other))
                return false;
            
            if (ReferenceEquals(this, other))
                return true;
            
            return Equals(other.display, display) && Equals(other.browsable, browsable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            
            if (ReferenceEquals(this, obj))
                return true;
            
            if (obj.GetType() != typeof (PreviousPageCommand))
                return false;
            
            return Equals((PreviousPageCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((display != null ? display.GetHashCode() : 0) * 397) ^
                       (browsable != null ? browsable.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PreviousPageCommand left, PreviousPageCommand right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PreviousPageCommand left, PreviousPageCommand right)
        {
            return ! Equals(left, right);
        }
    }
}