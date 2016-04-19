namespace Cavity.Data
{
    using System;
    using Xunit;

    public sealed class RepositoryExpectationsOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryExpectations<int>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryExpectations<int>());
        }

        [Fact]
        public void op_VerifyAll()
        {
            Assert.Throws<NotImplementedException>(() => new RepositoryExpectations<int>().VerifyAll<DummyRepository<int>>());
        }
    }
}