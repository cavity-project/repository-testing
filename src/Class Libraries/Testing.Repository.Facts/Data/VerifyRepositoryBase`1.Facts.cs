namespace Cavity.Data
{
    using Xunit;

    public sealed class VerifyRepositoryBaseOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<VerifyRepositoryBase<int>>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Record()
        {
            Assert.True(new PropertyExpectations<VerifyRepositoryBase<int>>(x => x.Record1)
                            .TypeIs<IRecord<int>>()
                            .Result);
        }
    }
}