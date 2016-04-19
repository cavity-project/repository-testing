namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordUrnNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecordUrnNull<int>>()
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
            Assert.NotNull(new RepositoryInsertRecordUrnNull<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecordUrnNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecordUrnNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryInsertRecordUrnNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrown()
        {
            var obj = new RepositoryInsertRecordUrnNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}