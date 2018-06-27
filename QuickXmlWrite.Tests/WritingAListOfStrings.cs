using System.Collections.Generic;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class WritingAListOfStrings
    {
        [Fact]
        public void Composed()
        {
            var intWriter = XmlWrite.For<string>().Tag("string").Content(x => x);
            var writer =
                from root in XmlWrite.For<string[]>().Tag("root")
                from sub in intWriter.Many()
                select root;
            var expected = "<root><string>42</string><string>666</string></root>";
            var actual = writer.Write(new[] { "42", "666" });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite.For<string[]>().Tag("root")
                let intWriter = XmlWrite.For<string>().Tag("string").Content(x => x)
                from sub in intWriter.Many()
                select root;
            var expected = "<root><string>42</string><string>666</string></root>";
            var actual = writer.Write(new[] { "42", "666" });
            Assert.Equal(expected, actual);
        }

        

        [Fact]
        public void ComposedFromObject()
        {
            var thing =
                new MyThing
                {
                    Name = "yep",
                    Stuff = new[] { "42", "666" }
                };

            var writer =
                from root in XmlWrite.For<MyThing>().Tag("root")
                from name in root.Tag("name").Content(x => x.Name)
                from codes in root.Tag("codes")
                from sub in codes.Many(x => x.Stuff,
                    from s in XmlWrite.For<string>().Tag("string")
                    from c in s.Tag("code")
                    from a in c.Attribute("value", x => x)
                    select s)
                select root;
            var expected = "<root><name>yep</name><codes><string><code value=\"42\" /></string><string><code value=\"666\" /></string></codes></root>";
            var actual = writer.Write(thing);
            Assert.Equal(expected, actual);
        }

        public class MyThing
        {
            public string Name { get; set; }
            public string[] Stuff { get; set; }
        }
    }
}