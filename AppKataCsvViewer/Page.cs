using System;
using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class Page
    {
        private const string WHITE_SPACE = " ";

        private readonly List<DataRecord> dataRecords = new List<DataRecord>();
        private PageFormatter pageFormatter;

        public Page(PageFormatter pageFormatter)
        {
            this.pageFormatter = pageFormatter;
        }

        public void Add(DataRecord dataRecord)
        {
            dataRecords.Add(dataRecord);
        }

        public string Header()
        {
            return pageFormatter.HeaderFor(dataRecords);
        }

        public string DataRecords()
        {
            return pageFormatter.Formatted(dataRecords);
        } 

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString() && GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            string page = String.Empty;

            foreach (var r in dataRecords)
                page += r + WHITE_SPACE;

            return page;
        }
    }
}