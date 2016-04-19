namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecord<int>>()
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
            Assert.NotNull(new RepositoryInsertRecord<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecord<RandomObject>
                          {
                              Record1 =
                                  {
                                      Created = DateTime.UtcNow,
                                      Key = AlphaDecimal.Random(),
                                      Modified = DateTime.UtcNow
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecord<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenCreatedIsNotSet()
        {
            var obj = new RepositoryInsertRecord<RandomObject>
                          {
                              Record1 =
                                  {
                                      Created = null,
                                      Key = AlphaDecimal.Random(),
                                      Modified = DateTime.UtcNow
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsNotSet()
        {
            var obj = new RepositoryInsertRecord<RandomObject>
                          {
                              Record1 =
                                  {
                                      Created = DateTime.UtcNow,
                                      Key = null,
                                      Modified = DateTime.UtcNow
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenModifiedIsNotSet()
        {
            var obj = new RepositoryInsertRecord<RandomObject>
                          {
                              Record1 =
                                  {
                                      Created = DateTime.UtcNow,
                                      Key = AlphaDecimal.Random(),
                                      Modified = null
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}