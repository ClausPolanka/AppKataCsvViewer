namespace AppKataCsvViewer
{
    public interface Browsable
    {
        Page NextPage();
        Page PreviousPage();
        int PageCount { get; }
        Page LastPage();
        Page FirstPage();
    }
}