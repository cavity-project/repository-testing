namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryMatchKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Match(AlphaDecimal.Random(), "\"{0}\"".FormatWith(Guid.NewGuid())))
            {
                throw new RepositoryTestException(Resources.Repository_Match_ReturnsTrue_ExceptionMessage);
            }
        }
    }
}