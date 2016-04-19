namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositorySelectUrnOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositorySelectUrn<int>>()
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
            Assert.NotNull(new RepositorySelectUrn<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositorySelectUrn<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = AlphaDecimal.Random()
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Select(obj.Record1.Urn))
                .Returns(obj.Record1)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositorySelectUrn<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsDifferent()
        {
            var obj = new RepositorySelectUrn<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = AlphaDecimal.Random()
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
                .Setup(x => x.Select(obj.Record1.Urn))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNullIsReturned()
        {
            var obj = new RepositorySelectUrn<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = AlphaDecimal.Random()
                                  }
                          };

            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Select(obj.Record1.Urn))
                .Returns(null as IRecord<RandomObject>)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUrnIsDifferent()
        {
            var obj = new RepositorySelectUrn<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = AlphaDecimal.Random()
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
                .Setup(x => x.Select(obj.Record1.Urn))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenValueIsDifferent()
        {
            var obj = new RepositorySelectUrn<RandomObject>
                          {
                              Record1 =
                                  {
                                      Key = AlphaDecimal.Random()
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
                .Setup(x => x.Select(obj.Record1.Urn))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}