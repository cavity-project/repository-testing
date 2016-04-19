namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositorySelectKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositorySelectKey<int>>()
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
            Assert.NotNull(new RepositorySelectKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<RandomObject>
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
                .Setup(x => x.Select(key))
                .Returns(obj.Record1)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositorySelectKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenInvalidOperationException()
        {
            var obj = new RepositorySelectKey<RandomObject>();

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<InvalidOperationException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<RandomObject>
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

            var record = new Mock<IRecord<RandomObject>>();
            record
                .SetupGet(x => x.Key)
                .Returns(AlphaDecimal.Random());
            record
                .SetupGet(x => x.Urn)
                .Returns(obj.Record1.Urn);
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNullIsReturned()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<RandomObject>
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
                .Setup(x => x.Select(key))
                .Returns(null as IRecord<RandomObject>)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUrnIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<RandomObject>
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

            var record = new Mock<IRecord<RandomObject>>();
            record
                .SetupGet(x => x.Key)
                .Returns(obj.Record1.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.net/" + Guid.NewGuid());
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenValueIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<RandomObject>
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

            var record = new Mock<IRecord<RandomObject>>();
            record
                .SetupGet(x => x.Key)
                .Returns(obj.Record1.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns(obj.Record1.Urn);
            record
                .SetupGet(x => x.Value)
                .Returns(new RandomObject(123));
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}