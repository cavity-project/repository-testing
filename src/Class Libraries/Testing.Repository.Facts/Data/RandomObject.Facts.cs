namespace Cavity.Data
{
    using System.Xml;
    using Xunit;

    public sealed class RandomObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RandomObject>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("random")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RandomObject());
        }

        [Fact]
        public void op_ToString()
        {
            XmlConvert.ToInt64(new RandomObject().ToString());
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<RandomObject>(x => x.Value)
                            .TypeIs<long>()
                            .XmlAttribute("value")
                            .Result);
        }
    }
}