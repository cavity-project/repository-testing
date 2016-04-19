namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class RepositoryTestException : Exception
    {
        public RepositoryTestException()
        {
        }

        public RepositoryTestException(string message)
            : base(message)
        {
        }

        public RepositoryTestException(string message,
                                       Exception innerException)
            : base(message, innerException)
        {
        }

        private RepositoryTestException(SerializationInfo info,
                                        StreamingContext context)
            : base(info, context)
        {
        }
    }
}