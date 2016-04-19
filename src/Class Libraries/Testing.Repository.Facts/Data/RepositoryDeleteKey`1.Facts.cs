namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryDeleteKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryDeleteKey<int>>()
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
            Assert.NotNull(new RepositoryDeleteKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = key
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Delete(key))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(key))
                .Returns(false)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryDeleteKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExists()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = key
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Delete(key))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(key))
                .Returns(true)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = key
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Delete(key))
                .Returns(false)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenInvalidOperationException()
        {
            var obj = new RepositoryDeleteKey<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<InvalidOperationException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}