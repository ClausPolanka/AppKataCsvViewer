using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class DataRecord
    {
        private readonly List<string> fields = new List<string>();

        public virtual void Add(string field)
        {
            fields.Add(field);
        }

        public virtual int ColumnCount { get { return fields.Count; } }
        public virtual List<string> Fields { get { return fields; } }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        public override string ToString()
        {
            string dataRecord = string.Empty;

            foreach (var w in Fields)
                dataRecord += w + " ";

            return dataRecord;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}