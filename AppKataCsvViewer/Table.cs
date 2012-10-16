using System.Collections.Generic;
using System.Linq;

namespace AppKataCsvViewer
{
    public class Table
    {
        private const int INDEX_OF_HEADER = 0;

        private readonly List<Page> pages = new List<Page>();

        public Table(List<DataRecord> dataRecords, int defaultPageSize)
        {
            var recordsCopy = new List<DataRecord>(dataRecords);
            this.Header = recordsCopy[INDEX_OF_HEADER];
            recordsCopy.RemoveAt(INDEX_OF_HEADER);
            this.pages = ToPages(recordsCopy, defaultPageSize);
        }

        private List<Page> ToPages(List<DataRecord> dataRecords, int pageSize)
        {
            List<Page> pages = new List<Page>();
            int pos = 0;
            
            while (dataRecords.Skip(pos).Any())
            {
                List<DataRecord> tmpRecords = dataRecords.Skip(pos).ToList();

                pages.Add(CreatePageOf(tmpRecords, pageSize));

                pos += pageSize;
            }
            return pages;
        }

        private Page CreatePageOf(List<DataRecord> dataRecords, int pageSize)
        {
            var page = new Page(new PageConsoleFormatter(new MaxConsoleColumnLengthsIdentifier()));
            page.Add(Header);

            foreach (DataRecord dataRecord in dataRecords.Take(pageSize))
                page.Add(dataRecord);

            return page;
        }

        public int PageCount { get { return pages.Count; } }
        public DataRecord Header { get; private set; }
        public List<Page> Pages { get { return pages; } }
    }
}