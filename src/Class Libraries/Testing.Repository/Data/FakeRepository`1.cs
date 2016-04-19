namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.Net;
    using Cavity.Security.Cryptography;

    public sealed class FakeRepository<T> : IRepository<T>
    {
        public FakeRepository()
        {
            Xml = new XmlDocument();
            Xml.LoadXml("<repository />");
        }

        private IRepository<T> Repository
        {
            get
            {
                return this;
            }
        }

        private XmlDocument Xml { get; set; }

        bool IRepository<T>.Delete(AbsoluteUri urn)
        {
            return Delete(Select(urn));
        }

        bool IRepository<T>.Delete(AlphaDecimal key)
        {
            return Delete(Select(key));
        }

        bool IRepository<T>.Exists(AbsoluteUri urn)
        {
            return null != Select(urn);
        }

        bool IRepository<T>.Exists(AlphaDecimal key)
        {
            return null != Select(key);
        }

        IRecord<T> IRepository<T>.Insert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Cacheability)
            {
                throw new RepositoryException();
            }

            if (null == record.Expiration)
            {
                throw new RepositoryException();
            }

            if (null == record.Status)
            {
                throw new RepositoryException();
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (record.Key.HasValue)
            {
                throw new RepositoryException();
            }

            if (Repository.Exists(record.Urn))
            {
                throw new RepositoryException();
            }

            var date = DateTime.UtcNow.ToXmlString();
            var key = AlphaDecimal.Random();
            var node = Xml.CreateElement("object");

            ////record.Urn = new AbsoluteUri(record.Urn.ToString().Replace("{token}", token));
            var attribute = Xml.CreateAttribute("cacheability");
            attribute.Value = record.Cacheability;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("created");
            attribute.Value = date;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("etag");
            attribute.Value = (EntityTag)MD5Hash.Compute(record.ToEntity());
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("expiration");
            attribute.Value = record.Expiration;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("key");
            attribute.Value = key;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("modified");
            attribute.Value = date;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("status");
            if (record.Status != null)
            {
                attribute.Value = XmlConvert.ToString(record.Status.Value);
            }

            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("urn");
            attribute.Value = record.Urn;
            node.Attributes.Append(attribute);

            attribute = Xml.CreateAttribute("type");
            attribute.Value = "{0}, {1}".FormatWith(record.GetType().FullName, record.GetType().Assembly.GetName().Name);
            node.Attributes.Append(attribute);

            node.InnerXml = record.Value.XmlSerialize().CreateNavigator().OuterXml;

            if (Xml.DocumentElement != null)
            {
                Xml.DocumentElement.AppendChild(node);
            }

            return Repository.Select(key);
        }

        bool IRepository<T>.Match(AbsoluteUri urn,
                                  EntityTag etag)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            var xpath = "{0}[@etag='{1}']".FormatWith(FormatXPath(urn), etag);
            var nodes = Xml.SelectNodes(xpath);
            if (null == nodes)
            {
                return false;
            }

            return 0 != nodes.Count;
        }

        bool IRepository<T>.Match(AlphaDecimal key,
                                  EntityTag etag)
        {
            var xpath = "{0}[@etag='{1}']".FormatWith(FormatXPath(key), etag);
            var nodes = Xml.SelectNodes(xpath);
            if (null == nodes)
            {
                return false;
            }

            return 0 != nodes.Count;
        }

        bool IRepository<T>.ModifiedSince(AbsoluteUri urn,
                                          DateTime value)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            var node = Select(urn);
            if (null == node ||
                null == node.Attributes)
            {
                return false;
            }

            var modified = XmlConvert.ToDateTime(node.Attributes["modified"].Value, XmlDateTimeSerializationMode.Utc);

            return modified > value;
        }

        bool IRepository<T>.ModifiedSince(AlphaDecimal key,
                                          DateTime value)
        {
            var node = Select(key);
            if (null == node ||
                null == node.Attributes)
            {
                return false;
            }

            var modified = XmlConvert.ToDateTime(node.Attributes["modified"].Value, XmlDateTimeSerializationMode.Utc);

            return modified > value;
        }

        IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (null == expression.Expression)
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            var result = new List<IRecord<T>>();

            var nodes = Xml.SelectNodes("/repository/object/node()");
            if (null == nodes)
            {
                return result;
            }

#if NET20
            foreach (XmlNode node in nodes)
            {
                if (null == node.ParentNode)
                {
                    continue;
                }

                var selection = node.ParentNode.SelectNodes(expression.Expression);
                if (null == selection)
                {
                    continue;
                }

                if (0 != selection.Count)
                {
                    result.Add(ToRecord(node.ParentNode));
                }
            }
#else
            result.AddRange(from XmlNode node in nodes
                            let parent = node.ParentNode
                            where parent != null
                            where null != parent
                            let selection = parent.SelectNodes(expression.Expression)
                            where null != selection
                            where 0 != selection.Count
                            select ToRecord(parent));
#endif

            return result;
        }

        IRecord<T> IRepository<T>.Select(AbsoluteUri urn)
        {
            return ToRecord(Select(urn));
        }

        IRecord<T> IRepository<T>.Select(AlphaDecimal key)
        {
            return ToRecord(Select(key));
        }

        AlphaDecimal? IRepository<T>.ToKey(AbsoluteUri urn)
        {
            var record = ToRecord(Select(urn));

            return null == record
                       ? null
                       : record.Key;
        }

        AbsoluteUri IRepository<T>.ToUrn(AlphaDecimal key)
        {
            var record = ToRecord(Select(key));

            return null == record
                       ? null
                       : record.Urn;
        }

        IRecord<T> IRepository<T>.Update(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Cacheability)
            {
                throw new RepositoryException();
            }

            if (null == record.Expiration)
            {
                throw new RepositoryException();
            }

            if (null == record.Status)
            {
                throw new RepositoryException();
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (!record.Key.HasValue)
            {
                throw new RepositoryException();
            }

            var keySelection = Repository.Select(record.Key.Value);
            if (null == keySelection)
            {
                throw new RepositoryException();
            }

            var urnSelection = Repository.Select(record.Urn);
            if (null != urnSelection &&
                keySelection.Key != urnSelection.Key)
            {
                throw new RepositoryException();
            }

            var node = Select(record.Key.Value);
            if (null == node)
            {
                return null;
            }

            if (node.Attributes != null)
            {
                node.Attributes["cacheability"].Value = record.Cacheability;
                node.Attributes["etag"].Value = (EntityTag)MD5Hash.Compute(record.ToEntity());
                node.Attributes["expiration"].Value = record.Expiration;
                node.Attributes["modified"].Value = DateTime.UtcNow.ToXmlString();
                if (record.Status != null)
                {
                    node.Attributes["status"].Value = XmlConvert.ToString(record.Status.Value);
                }

                node.Attributes["type"].Value = "{0}, {1}".FormatWith(record.GetType().FullName, record.GetType().Assembly.GetName().Name);
                node.Attributes["urn"].Value = record.Urn;
            }

            node.InnerXml = record.Value.XmlSerialize().CreateNavigator().OuterXml;

            return record.Key != null
                       ? Repository.Select(record.Key.Value)
                       : null;
        }

        IRecord<T> IRepository<T>.Upsert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            return null == record.Key
                       ? Repository.Insert(record)
                       : Repository.Update(record);
        }

        private static bool Delete(XmlNode node)
        {
            if (null == node)
            {
                return false;
            }

            var parent = node.ParentNode;
            if (null != parent)
            {
                parent.RemoveChild(node);
                return true;
            }

            return false;
        }

        private static string FormatXPath(AbsoluteUri urn)
        {
            return "/repository/object[@urn='{0}']".FormatWith(urn);
        }

        private static string FormatXPath(AlphaDecimal key)
        {
            return "/repository/object[@key='{0}']".FormatWith(key);
        }

        private static IRecord<T> ToRecord(XmlNode node)
        {
            if (null == node)
            {
                return null;
            }

            if (null == node.Attributes)
            {
                return null;
            }

            return new Record<T>
                       {
                           Cacheability = node.Attributes["cacheability"].Value,
                           Created = XmlConvert.ToDateTime(node.Attributes["created"].Value, XmlDateTimeSerializationMode.Utc),
                           Etag = node.Attributes["etag"].Value,
                           Expiration = node.Attributes["expiration"].Value,
                           Key = AlphaDecimal.FromString(node.Attributes["key"].Value),
                           Modified = XmlConvert.ToDateTime(node.Attributes["modified"].Value, XmlDateTimeSerializationMode.Utc),
                           Status = XmlConvert.ToInt32(node.Attributes["status"].Value),
                           Urn = node.Attributes["urn"].Value,
                           Value = node.InnerXml.XmlDeserialize<T>()
                       };
        }

        private XmlNode Select(AbsoluteUri urn)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            return Xml.SelectSingleNode(FormatXPath(urn));
        }

        private XmlNode Select(AlphaDecimal key)
        {
            return Xml.SelectSingleNode(FormatXPath(key));
        }
    }
}