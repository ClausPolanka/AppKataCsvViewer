using System.Collections.Generic;
using System.Linq;

namespace AppKataCsvViewer
{
    public class Table
    {
        private readonly int pageSize;
        private const int INDEX_OF_HEADER = 0;

        private readonly List<Page> pages = new List<Page>();

        public Table(List<DataRecord> dataRecords, int pageSize)
        {
            this.pageSize = pageSize;
            SeperateIntoPages(dataRecords);
        }

        private void SeperateIntoPages(List<DataRecord> dataRecords)
        {
            List<DataRecord> recordsCopy = new List<DataRecord>(dataRecords);
            Header = dataRecords[INDEX_OF_HEADER];
            recordsCopy.RemoveAt(INDEX_OF_HEADER);
            int pos = 0;
            
            while (recordsCopy.Skip(pos).Any())
            {
                recordsCopy = recordsCopy.Skip(pos).ToList();

                var page = new Page();
                page.Add(Header);

                foreach (DataRecord dataRecord in recordsCopy.Take(pageSize))
                    page.Add(dataRecord);

                pages.Add(page);
                pos = pageSize;
            }
        }

        public int PageCount { get { return pages.Count; } }
        public DataRecord Header { get; private set; }
        public List<Page> Pages { get { return pages; } }
    }
}