namespace Cavity.Data
{
    using System;
    using Moq;
    using Xunit;

    public sealed class RepositoryToKeyUrnOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryToKeyUrn<int>>()
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
            Assert.NotNull(new RepositoryToKeyUrn<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryToKeyUrn<RandomObject>
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
                .Setup(x => x.ToKey(obj.Record1.Urn))
                .Returns(obj.Record1.Key)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryToKeyUrn<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsDifferent()
        {
            var obj = new RepositoryToKeyUrn<RandomObject>
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
                .Setup(x => x.ToKey(obj.Record1.Urn))
                .Returns(AlphaDecimal.Random())
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNullIsReturned()
        {
            var obj = new RepositoryToKeyUrn<RandomObject>
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
                .Setup(x => x.ToKey(obj.Record1.Urn))
                .Returns(null as AlphaDecimal?)
                .Verifiable();

            Assert.Throws<RepositoryTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}