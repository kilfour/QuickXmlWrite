using Xunit;

namespace QuickXmlWrite.Tests
{
    public class ComposingWriters
    {
        [Fact]
        public void Example()
        {
            var intWriter = XmlWrite<int>.Tag("int").Content(x => x.ToString());

            var writer =
                from root in XmlWrite<int>.Tag("root")
                from sub in intWriter
                select root;

            var expected = "<root><int>42</int></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }
    }
}