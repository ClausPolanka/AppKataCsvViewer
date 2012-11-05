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
        public virtual void Execute()
        {
            display.Show(browsable.FirstPage());
            display.PrintUserOptionsFor(browsable.PageCount);
        }

        public virtual bool IsNotExitCommand()
        {
            return true;
        }

        public virtual bool Equals(FirstPageCommand other)
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
            
            if (obj.GetType() != typeof (FirstPageCommand))
                return false;
            
            return Equals((FirstPageCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((display != null ? display.GetHashCode() : 0) * 397) ^
                       (browsable != null ? browsable.GetHashCode() : 0);
            }
        }

        public static bool operator ==(FirstPageCommand left, FirstPageCommand right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FirstPageCommand left, FirstPageCommand right)
        {
            return ! Equals(left, right);
        }
    }
}