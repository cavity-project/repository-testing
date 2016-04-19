namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Cavity.Net;

    public sealed class DummyRepository<T> : IRepository<T>
    {
        public bool Exists(XPathExpression expression)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        public bool Exists(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        public IRecord<T> Insert(IRecord<T> record)
        {
            throw new NotImplementedException();
        }

        public bool Match(AbsoluteUri urn,
                          EntityTag etag)
        {
            throw new NotImplementedException();
        }

        public bool Match(AlphaDecimal key,
                          EntityTag etag)
        {
            throw new NotImplementedException();
        }

        public bool ModifiedSince(AbsoluteUri urn,
                                  DateTime value)
        {
            throw new NotImplementedException();
        }

        public bool ModifiedSince(AlphaDecimal key,
                                  DateTime value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRecord<T>> Query(XPathExpression expression)
        {
            throw new NotImplementedException();
        }

        public IRecord<T> Select(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        public IRecord<T> Select(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        public AlphaDecimal? ToKey(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        public AbsoluteUri ToUrn(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        public IRecord<T> Update(IRecord<T> record)
        {
            throw new NotImplementedException();
        }

        public IRecord<T> Upsert(IRecord<T> record)
        {
            throw new NotImplementedException();
        }
    }
}