using System;
using System.Collections.Generic;
using System.Linq;

namespace AppKataCsvViewer
{
    public class Table : Browsable
    {
        private const int INDEX_OF_HEADER = 0;

        private readonly List<Page> pages = new List<Page>();
        
        private int pageNumber;
        private bool WasExecutedFirstTime = true;

        public Table(List<DataRecord> dataRecords, int defaultPageSize)
        {
            if (dataRecords == null || ! dataRecords.Any())
                throw new Exception("There must be at least one data record.");

            if (defaultPageSize < 1)
                throw new Exception("Default page size must be higher than 0.");

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

        public Page NextPage()
        {
            if (WasExecutedFirstTime)
            {
                WasExecutedFirstTime = false;
                return Pages[pageNumber];
            }

            if (pageNumber == Pages.Count - 1)
            {
                pageNumber = 0;
                return Pages[0];
            }

            return Pages[++pageNumber];
        }

        public Page PreviousPage()
        {
            if (WasExecutedFirstTime)
                WasExecutedFirstTime = false;

            if (pageNumber == 0)
            {
                pageNumber = Pages.Count - 1;
                return Pages.Last();
            }

            return Pages[--pageNumber];
        }
        
        public Page LastPage()
        {
            if (WasExecutedFirstTime)
                WasExecutedFirstTime = false;

            pageNumber = Pages.Count - 1;
            return Pages.Last();
        }

        public Page FirstPage()
        {
            if (WasExecutedFirstTime)
                WasExecutedFirstTime = false;

            pageNumber = 0;
            return Pages.First();
        }

        public int PageCount { get { return pages.Count; } }
        public DataRecord Header { get; private set; }
        public List<Page> Pages { get { return pages; } }
    }
}