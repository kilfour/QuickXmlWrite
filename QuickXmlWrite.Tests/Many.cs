using System.Collections.Generic;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Many
    {
        [Fact]
        public void Composed()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite.For<int[]>().Tag("root")
                from sub in intWriter.Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] {42, 666});
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite.For<int[]>().Tag("root")
                let intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString())
                from sub in intWriter.Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] { 42, 666 });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChainedTyped()
        {
            var writer = XmlWrite.For<TheThing>().Tag("root").Many(x => x.TheInts, y => y.Tag("int").Content(x => x.ToString()));
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write<TheThing>(new TheThing());
            Assert.Equal(expected, actual);
        }


        public class TheThing { public List<int> TheInts = new List<int> { 42, 666 }; }
    }
}