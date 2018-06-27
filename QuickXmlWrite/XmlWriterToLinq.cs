using QuickXmlWrite.UnderTheHood;
using System;

namespace QuickXmlWrite
{
    public static class XmlWriterToLinq
    {
        public static XmlWriter<TValueTwo> Select<TValueOne, TValueTwo>(
            this XmlWriter<TValueOne> generator,
            Func<TValueOne, TValueTwo> selector)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return s => new Result<TValueTwo>(selector(generator(s).Value), s);
        }

        public static XmlWriter<TValueTwo> SelectMany<TValueOne, TValueTwo>(
            this XmlWriter<TValueOne> generator,
            Func<TValueOne, XmlWriter<TValueTwo>> selector)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return s => selector(generator(s).Value)(s);
        }

        public static XmlWriter<TValueThree> SelectMany<TValueOne, TValueTwo, TValueThree>(
            this XmlWriter<TValueOne> generator,
            Func<TValueOne, XmlWriter<TValueTwo>> selector,
            Func<TValueOne, TValueTwo, TValueThree> projector)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            if (projector == null)
                throw new ArgumentNullException(nameof(projector));

            return generator.SelectMany(x => selector(x).Select(y => projector(x, y)));
        }
    }
}
