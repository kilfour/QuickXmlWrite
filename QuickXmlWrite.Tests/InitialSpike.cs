using Xunit;

namespace QuickXmlWrite.Tests
{
    public class InitialSpike
    {   
        [Fact]
        public void JustTheRoot()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root")
                select root;
            var expected = "<root></root>";
            var actual = writer.Write("");
           Assert.Equal(expected, actual); 
        }

        [Fact]
        public void WithDynamicRoot()
        {
            var writer =
                from root in XmlWrite<string>.Tag(x => x as string)
                select root;
            var expected = "<dynamic></dynamic>";
            var actual = writer.Write("dynamic");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithSomeContent()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root")
                from content in root.Content<string>("just some text")
                select root;
            var expected = "<root>just some text</root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithDynamicContent()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root")
                from content in root.Content<string>(x => x)
                select root;
            var expected = "<root>dynamic</root>";
            var actual = writer.Write("dynamic");
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Nested()
        {
            var writer =
                from root in XmlWrite<string>.Tag("root")
                from child in root.Tag<string>("child")
                from content in child.Content<string>("just some text")
                select root;
            var expected = "<root><child>just some text</child></root>";
            var actual = writer.Write("");
            Assert.Equal(expected, actual);
        }

    }
}
