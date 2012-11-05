namespace AppKataCsvViewer
{
    public interface UserCommands
    {
        void Execute(UserCommand command);
        void FirstPage();
    }
}