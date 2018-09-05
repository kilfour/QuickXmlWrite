using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using QuickXmlWrite.UnderTheHood;
using Xunit;

namespace QuickXmlWrite.Tests
{
    public class Many
    {
        [Fact]
        public void Composed()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite.For<int[]>().Tag("root")
                from sub in intWriter.Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] {42, 666});
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inline()
        {
            var writer =
                from root in XmlWrite.For<int[]>().Tag("root")
                let intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString())
                from sub in intWriter.Many()
                select root;
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new[] { 42, 666 });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChainedTyped()
        {
            var writer = XmlWrite.For<TheThing>().Tag("root").Many(x => x.TheInts, y => y.Tag("int").Content(x => x.ToString()));
            var expected = "<root><int>42</int><int>666</int></root>";
            var actual = writer.Write(new TheThing());
            Assert.Equal(expected, actual);
        }


        public class TheThing { public List<int> TheInts = new List<int> { 42, 666 }; }

        [Fact]
        public void Nested()
        {
            var writer =
                from root in XmlWrite.For<TheOtherThing>().Tag("root")
                from things in root.Many(x => x.Things,
                    r =>
                        from i in r.Tag("thing")
                            .Many(t => t.TheInts,
                                i => i.Tag("int").Content(x => x.ToString()))
                        select i
                    )
                select root;
            var expected = "<root><thing><int>42</int><int>666</int></thing><thing><int>42</int><int>666</int></thing></root>";
            var actual = writer.Write(new TheOtherThing());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NestedWithSeperateWriter()
        {
            var intWriter = XmlWrite.For<int>().Tag("int").Content(x => x.ToString());
            var writer =
                from root in XmlWrite.For<TheOtherThing>().Tag("root")
                from things in root.Many(x => x.Things,
                    r =>
                        from i in r.Tag("thing")
                            .Many(t => t.TheInts,
                                node =>
                                    from child in node.Tag("child")
                                    from iw in intWriter
                                    select child)
                        select i
                )
                select root;
            var expected = "<root><thing><child><int>42</int></child><child><int>666</int></child></thing><thing><child><int>42</int></child><child><int>666</int></child></thing></root>";
            var actual = writer.Write(new TheOtherThing());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NestedNested()
        {
            var writer =
                from root in XmlWrite.For<TheOtherThing>().Tag("root")
                from things in root.Many(x => x.Things,
                    r =>
                        from i in r.Tag("thing")
                            .Many(t => t.TheInts,
                                node =>
                                    from children in node.Tag("children")
                                    from child in children.Tag("child")
                                    from iw in child.Tag("int").Content(x => x.ToString())
                                    select children)
                        select i
                )
                select root;
            var expected = "<root><thing><children><child><int>42</int></child></children><children><child><int>666</int></child></children></thing><thing><children><child><int>42</int></child></children><children><child><int>666</int></child></children></thing></root>";
            var actual = writer.Write(new TheOtherThing());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NestedNesteWithSeperateWriterAsMethod()
        {
            var writer =
                from root in XmlWrite.For<TheOtherThing>().Tag("root")
                from things in root.Many(x => x.Things,
                    r =>
                        from i in r.Tag("thing")
                            .Many(t => t.TheInts,
                                node =>
                                    from children in node.Tag("children")
                                    from child in children.Tag("child")
                                    from iw in GetIntWriter("int", x => x.ToString())
                                    select children)
                        select i
                )
                select root;
            var expected = "<root><thing><children><child><int>42</int></child></children><children><child><int>666</int></child></children></thing><thing><children><child><int>42</int></child></children><children><child><int>666</int></child></children></thing></root>";
            var actual = writer.Write(new TheOtherThing());
            Assert.Equal(expected, actual);
        }

        public XmlWriter<XmlWriterNode<int>> GetIntWriter(string tag, Func<int, string>  func)
        {
            return XmlWrite.For<int>().Tag(tag).Content(func);
        }

        public class TheOtherThing { public List<TheThing> Things = new List<TheThing> {new TheThing(), new TheThing() }; }
    }
}