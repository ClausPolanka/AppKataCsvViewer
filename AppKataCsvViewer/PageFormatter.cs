using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public interface PageFormatter
    {
        string HeaderFor(List<DataRecord> dataRecords);
        string Formatted(List<DataRecord> dataRecords);
    }
}