using System.Collections.Generic;

namespace AppKataCsvViewer
{
    public class DataRecord
    {
        private readonly List<string> words = new List<string>();

        public void Add(string word)
        {
            words.Add(word);
        }

        public int ColumnCount { get { return words.Count; } }
        public List<string> Words { get { return words; } }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        public override string ToString()
        {
            string dataRecord = string.Empty;

            foreach (var w in Words)
                dataRecord += w + " ";

            return dataRecord;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}