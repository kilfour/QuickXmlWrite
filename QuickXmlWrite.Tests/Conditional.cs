using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Conditional
    {
        [Fact]
        public void IsTrue()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite.For<int>().Tag("root")
                from sub in root.Tag("int").Content(x => x.ToString()).If(x => x == 42)
                select root;
            var expected = "<root><int>42</int></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsFalse()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite.For<int>().Tag("root")
                from sub in root.Tag("int").Content(x => x.ToString()).If(x => x == 42)
                select root;
            var expected = "<root><int>42</int></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }
    }
}