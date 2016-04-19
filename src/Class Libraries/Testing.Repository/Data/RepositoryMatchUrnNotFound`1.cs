namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryMatchUrnNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Match("urn://example.com/" + Guid.NewGuid(), "\"{0}\"".FormatWith(Guid.NewGuid())))
            {
                throw new RepositoryTestException(Resources.Repository_Match_ReturnsTrue_ExceptionMessage);
            }
        }
    }
}