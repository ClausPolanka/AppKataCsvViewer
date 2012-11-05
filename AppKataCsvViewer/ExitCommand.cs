namespace AppKataCsvViewer
{
    public class ExitCommand : UserCommand
    {
        public virtual void Execute()
        {
        }

        public virtual bool IsNotExitCommand()
        {
            return false;
        }
    }
}