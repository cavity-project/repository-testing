namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryToUrnKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var urn = repository.ToUrn(AlphaDecimal.Random());

            if (null != urn)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectNullWhenRecordNotFound_ExceptionMessage.FormatWith("ToUrn", "key"));
            }
        }
    }
}