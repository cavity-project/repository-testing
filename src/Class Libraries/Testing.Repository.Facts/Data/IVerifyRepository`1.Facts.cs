namespace Cavity.Data
{
    using Moq;
    using Xunit;

    public sealed class IVerifyRepositoryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IVerifyRepository<int>>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>().Object;

            var mock = new Mock<IVerifyRepository<int>>();
            mock
                .Setup(x => x.Verify(repository))
                .Verifiable();

            mock.Object.Verify(repository);

            mock.VerifyAll();
        }
    }
}