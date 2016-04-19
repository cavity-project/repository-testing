namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryModifiedSinceUrnOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryModifiedSinceUrn<int>>()
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
            Assert.NotNull(new RepositoryModifiedSinceUrn<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryModifiedSinceUrn<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(obj.Record1.Urn, DateTime.MinValue))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(obj.Record1.Urn, DateTime.MaxValue))
                .Returns(false)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryModifiedSinceUrn<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenModifiedSinceMaxDate()
        {
            var obj = new RepositoryModifiedSinceUrn<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(obj.Record1.Urn, DateTime.MaxValue))
                .Returns(true)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNotModifiedSinceMinDate()
        {
            var obj = new RepositoryModifiedSinceUrn<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(obj.Record1.Urn, DateTime.MaxValue))
                .Returns(false)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(obj.Record1.Urn, DateTime.MinValue))
                .Returns(false)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}