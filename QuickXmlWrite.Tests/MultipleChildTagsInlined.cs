using QuickXmlWrite.UnderTheHood;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class MultipleChildTagsInlined
    {
        [Fact]
        public void Fluent()
        {
            var writer =
                XmlWrite.For<MyThing>()
                    .Tag("root")
                    .Sequence(
                        x => x.Tag("fred"),
                        x => x.Tag("jos"));

            var expected = "<root><fred/><jos/></root>";
            var actual = writer.Write(new MyThing());
            Assert.Equal(expected, actual);
        }

        public class MyThing
        {
            public string MyString { get; set; }
            public int MyInt { get; set; }
        }
    }
}