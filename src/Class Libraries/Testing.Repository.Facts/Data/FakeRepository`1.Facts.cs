namespace Cavity.Data
{
    using Xunit;

    public sealed class FakeRepositoryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FakeRepository<int>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IRepository<int>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FakeRepository<int>());
        }

        [Fact]
        public void expectations_class()
        {
            new RepositoryExpectations<RandomObject>().VerifyAll<FakeRepository<RandomObject>>();
        }

        [Fact]
        public void expectations_struct()
        {
            new RepositoryExpectations<int>().VerifyAll<FakeRepository<int>>();
        }
    }
}