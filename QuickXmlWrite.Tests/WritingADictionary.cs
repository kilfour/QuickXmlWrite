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
                from root in XmlWrite<Dictionary<string,string>>.Tag("root")
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
    }
}