using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfStrings
    {
        [Fact]
        public void Composed()
        {
            var intWriter = XmlWrite<string>.Tag("string").Content(x => x);
            var writer =
                from root in XmlWrite<List<string>>.Tag("root")
                from sub in intWriter.Many()
                select root;
            var expected = "<root><string>42</string><string>666</string></root>";
            var actual = writer.Write(new[] { "42", "666" });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite<List<string>>.Tag("root")
                let intWriter = XmlWrite<string>.Tag("string").Content(x => x)
                from sub in intWriter.Many()
                select root;
            var expected = "<root><string>42</string><string>666</string></root>";
            var actual = writer.Write(new[] { "42", "666" });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BetterInline()
        {
            var writer =
                from root in XmlWrite<IEnumerable<string>>.Tag("root")
                from sub in root.ApplyMany(x => x.Tag("string").Content(y => y))
                select root;
            var expected = "<root><string>42</string><string>666</string></root>";
            var actual = writer.Write(new[] { "42", "666" });
            Assert.Equal(expected, actual);
        }
    }
}