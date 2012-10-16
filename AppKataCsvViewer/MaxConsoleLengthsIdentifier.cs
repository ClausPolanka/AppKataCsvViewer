using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public interface MaxConsoleLengthsIdentifier
    {
        int[] MaxColumnLengthsOf(List<DataRecord> dataRecords);
    }
}