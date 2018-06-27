using System.Collections.Generic;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingADictionary
    {
        [Fact]
        public void Explicit()
        {
            var writer =
                from root in XmlWrite.For<IDictionary<string, string>>().Tag("root")
                from k1 in root.Tag("keyone").Content(x => x["keyone"])
                from k2 in root.Tag("keytwo").Content(x => x["keytwo"])
                select root;
            var expected = "<root><keyone>valueone</keyone><keytwo>valuetwo</keytwo></root>";
            var actual = 
                writer.Write(new Dictionary<string, string>
                {
                    {"keyone","valueone" },
                    {"keytwo","valuetwo" }
                });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ByKeyValue()
        {
            var keyValueWriter = XmlWrite.For<KeyValuePair<string, string>>().Tag(x => x.Key).Content(x => x.Value);
            var writer =
                from root in XmlWrite.For<IDictionary<string, string>>().Tag("root")
                from sub in keyValueWriter.Many()
                select root;
            var expected = "<root><keyone>valueone</keyone><keytwo>valuetwo</keytwo></root>";
            var actual =
                writer.Write(new Dictionary<string, string>
                {
                    {"keyone","valueone" },
                    {"keytwo","valuetwo" }
                });
            Assert.Equal(expected, actual);
        }
    }
}