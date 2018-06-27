using Xunit;

namespace QuickXmlWrite.Tests
{
    public class ComposingWriters
    {
        [Fact]
        public void OneLevel()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());

            var writer =
                from root in XmlWrite.For<int>().Tag("root")
                from sub in intWriter
                select root;

            var expected = "<root><int>42</int></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TwoLevels()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());

            var subWriter =
                from root in XmlWrite.For<int>().Tag("sub")
                from sub in intWriter
                select root;

            var writer =
                from root in XmlWrite.For<int>().Tag("root")
                from sub in subWriter
                select root;

            var expected = "<root><sub><int>42</int></sub></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }
    }
}