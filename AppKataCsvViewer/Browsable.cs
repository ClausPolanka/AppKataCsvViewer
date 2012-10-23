namespace AppKataCsvViewer
{
    public interface Browsable
    {
        Page NextPage();
        Page PreviousPage();
        int PageCount { get; }
    }
}