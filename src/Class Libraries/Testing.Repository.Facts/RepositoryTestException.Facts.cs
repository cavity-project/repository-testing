namespace Cavity
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class RepositoryTestExceptionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryTestException>()
                            .DerivesFrom<Exception>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryTestException());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new RepositoryTestException("test");
            RepositoryTestException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new RepositoryTestException("test"));
                stream.Position = 0;
                actual = (RepositoryTestException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new RepositoryTestException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new RepositoryTestException(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new RepositoryTestException(string.Empty, null));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new RepositoryTestException(null));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new RepositoryTestException(null, null));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new RepositoryTestException("message", new InvalidOperationException()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new RepositoryTestException("message", null));
        }
    }
}