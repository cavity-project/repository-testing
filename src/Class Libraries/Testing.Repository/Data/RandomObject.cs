namespace Cavity.Data
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("random")]
    public sealed class RandomObject : ComparableObject
    {
        public RandomObject()
            : this(DateTime.UtcNow.Ticks)
        {
        }

        public RandomObject(long value)
        {
            Value = value;
        }

        [XmlAttribute("value")]
        public long Value { get; set; }

        public override string ToString()
        {
            return XmlConvert.ToString(Value);
        }
    }
}