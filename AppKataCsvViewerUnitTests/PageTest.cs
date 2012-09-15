using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerUnitTests
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void MaxColumnLengths_GivenAPageWith2Records_ReturnMaxLengthForEachColumn()
        {
            var header = new DataRecord();
            header.Add("Name");
            header.Add("Age");
            header.Add("City");

            var dataRecord = new DataRecord();
            dataRecord.Add("Claus");
            dataRecord.Add("30");
            dataRecord.Add("Vienna");

            var page = new Page();
            page.Add(header);
            page.Add(dataRecord);

            int[] expected = { "Claus".Length, "Age".Length, "Vienna".Length };

            Assert.AreEqual(expected, page.MaxColumnLengths(), "page max column lengths");
        }
    }
}
