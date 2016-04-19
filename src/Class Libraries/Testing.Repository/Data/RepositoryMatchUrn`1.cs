namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryMatchUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            Record1 = repository.Insert(Record1);

            if (repository.Match(Record1.Urn, Record1.Etag))
            {
                return;
            }

            throw new RepositoryTestException(Resources.Repository_Match_ReturnsFalse_ExceptionMessage);
        }
    }
}