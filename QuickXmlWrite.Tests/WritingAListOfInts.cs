using System.Collections.Generic;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfInts
    {
        [Fact]
        public void Composed()
        {
            var intWriter = XmlWrite<int>.Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite<List<int>>.Tag("root")
                from sub in intWriter.Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] {42, 666});
            Assert.Equal(expected, actual);
        }

        [Fact (Skip = "WIP")]
        public void Inline()
        {
            var writer =
                from root in XmlWrite<List<int>>.Tag("root")
                from sub in root.Tag("int").Content(x => x.ToString()).Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] { 42, 666 });
            Assert.Equal(expected, actual);
        }
    }
}