using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class CsvTable
    {
        private readonly List<List<string>> formattedRows;
        private readonly int pageSize;
        private readonly int[] maxColumnLengths;

        public CsvTable(List<List<string>> formattedRows, int pageSize, int[] maxColumnLengths)
        {
            this.formattedRows = formattedRows;
            this.pageSize = pageSize;
            this.maxColumnLengths = maxColumnLengths;
        }

        public List<List<string>> FormattedRows
        {
            get { return formattedRows; }
        }

        public int PageSize
        {
            get { return pageSize; }
        }

        public int[] MaxColumnLengths
        {
            get { return maxColumnLengths; }
        }
    }
}