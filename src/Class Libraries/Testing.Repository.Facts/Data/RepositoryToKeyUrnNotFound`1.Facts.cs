namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    public sealed class RepositoryToKeyUrnNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryToKeyUrnNotFound<int>>()
                            .DerivesFrom<VerifyRepositoryBase<int>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IVerifyRepository<int>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryToKeyUrnNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.ToKey(It.IsAny<AbsoluteUri>()))
                .Returns(null as AlphaDecimal?)
                .Verifiable();

            new RepositoryToKeyUrnNotFound<RandomObject>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryToKeyUrnNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsReturned()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.ToKey(It.IsAny<AbsoluteUri>()))
                .Returns(AlphaDecimal.Random())
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => new RepositoryToKeyUrnNotFound<RandomObject>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}