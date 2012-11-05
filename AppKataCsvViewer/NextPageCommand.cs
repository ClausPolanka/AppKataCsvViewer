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

        public virtual void Execute()
        {
            display.Show(browsable.NextPage());
            display.PrintUserOptionsFor(browsable.PageCount);
        }

        public virtual bool IsNotExitCommand()
        {
            return true;
        }

        public virtual bool Equals(NextPageCommand other)
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
            
            if (obj.GetType() != typeof (NextPageCommand))
                return false;
            
            return Equals((NextPageCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((display != null ? display.GetHashCode() : 0) * 397) ^
                       (browsable != null ? browsable.GetHashCode() : 0);
            }
        }

        public static bool operator ==(NextPageCommand left, NextPageCommand right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NextPageCommand left, NextPageCommand right)
        {
            return ! Equals(left, right);
        }
    }
}