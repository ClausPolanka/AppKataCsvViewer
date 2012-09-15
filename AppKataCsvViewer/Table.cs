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
            this.pages = ToPages(new List<DataRecord>(dataRecords));
        }

        private List<Page> ToPages(List<DataRecord> dataRecords)
        {
            Header = dataRecords[INDEX_OF_HEADER];
            dataRecords.RemoveAt(INDEX_OF_HEADER);
            List<Page> pages = new List<Page>();
            int pos = 0;
            
            while (dataRecords.Skip(pos).Any())
            {
                List<DataRecord> tmpRecords = dataRecords.Skip(pos).ToList();

                pages.Add(CreatePageOf(tmpRecords));

                pos += pageSize;
            }
            return pages;
        }

        private Page CreatePageOf(List<DataRecord> dataRecords)
        {
            var page = new Page();
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