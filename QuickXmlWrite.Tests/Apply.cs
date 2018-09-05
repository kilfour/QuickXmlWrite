using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Apply
    {
        [Fact]
        public void Spike()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());

            var writer =
                from root in XmlWrite.For<int>().Tag("root")
                from sub in root.Apply(intWriter)
                select root;

            var expected = "<root><int>42</int></root>";
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }
    }
}