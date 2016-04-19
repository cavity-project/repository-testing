namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    public sealed class RepositoryExistsUrnNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryExistsUrnNotFound<int>>()
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
            Assert.NotNull(new RepositoryExistsUrnNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Exists(It.IsAny<AbsoluteUri>()))
                .Returns(false)
                .Verifiable();

            new RepositoryExistsUrnNotFound<RandomObject>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryExistsUrnNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenTrue()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Exists(It.IsAny<AbsoluteUri>()))
                .Returns(true)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => new RepositoryExistsUrnNotFound<RandomObject>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}