namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryModifiedSinceUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            if (repository.ModifiedSince(Record1.Urn, DateTime.MaxValue))
            {
                throw new RepositoryTestException(Resources.Repository_ModifiedSince_ReturnsTrue_ExceptionMessage);
            }

            if (repository.ModifiedSince(Record1.Urn, DateTime.MinValue))
            {
                return;
            }

            throw new RepositoryTestException(Resources.Repository_ModifiedSince_ReturnsFalse_ExceptionMessage);
        }
    }
}