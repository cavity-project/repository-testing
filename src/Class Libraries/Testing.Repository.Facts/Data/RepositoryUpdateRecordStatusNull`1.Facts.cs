namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    public sealed class RepositoryUpdateRecordStatusNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryUpdateRecordStatusNull<int>>()
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
            Assert.NotNull(new RepositoryUpdateRecordStatusNull<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryUpdateRecordStatusNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
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
            Assert.Throws<ArgumentNullException>(() => new RepositoryUpdateRecordStatusNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsNotThrown()
        {
            var obj = new RepositoryUpdateRecordStatusNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2))
                .Returns(new Mock<IRecord<RandomObject>>().Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsUnexpectedlyThrown()
        {
            var obj = new RepositoryUpdateRecordStatusNull<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}