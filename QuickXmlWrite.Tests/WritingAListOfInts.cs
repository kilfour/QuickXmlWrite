using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfInts
    {
        [Fact(Skip="WIP")]
        public void Verbose()
        {
            var writer =
                from root in XmlWrite<int>.Tag("root")
                from tag in root.Tag("int").Content(x => x.ToString())
                select root;

            var expected = "<root><string>some text</string><int>42</int></root>";

            
            var actual = writer.Write(42);
            Assert.Equal(expected, actual);
        }
    }
}