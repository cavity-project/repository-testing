namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryUpdateRecordUrnExistsOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryUpdateRecordUrnExists<int>>()
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
            Assert.NotNull(new RepositoryUpdateRecordUrnExists<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryUpdateRecordUrnExists<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryUpdateRecordUrnExists<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryUpdateRecordUrnExists<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnDuplicateRecord()
        {
            var obj = new RepositoryUpdateRecordUrnExists<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnInitialRecord()
        {
            var obj = new RepositoryUpdateRecordUrnExists<RandomObject>();

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