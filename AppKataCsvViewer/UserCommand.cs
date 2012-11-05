namespace AppKataCsvViewer
{
    public interface UserCommand
    {
        void Execute();
        bool IsNotExitCommand();
    }
}