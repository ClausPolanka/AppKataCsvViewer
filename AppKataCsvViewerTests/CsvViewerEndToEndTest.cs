using System;
using System.IO;
using AppKataCsvViewer;
using NUnit.Framework;

namespace AppKataCsvViewerTests
{
    [TestFixture]
    public class CsvViewerEndToEndTest
    {
        [Test]
        public void ForADefaultPageSizeOf3_ViewerShowsOnePageForGivenCsvData()
        {
            // TODO: Create temporary persons.csv file
            
            MainEntryPoint.Main(new[] { "persons.csv" });

            // Assert against redirected console output.

            // TODO: Delete temporary csv file.
        }

        [SetUp]
        public virtual void SetUp()
        {
            // Redirect Console.Out = ??;
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.Out.Close();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
    }
}
