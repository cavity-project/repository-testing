namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositorySelectUrnNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Select("urn://example.com/" + Guid.NewGuid());

            if (null != record)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectNullWhenRecordNotFound_ExceptionMessage.FormatWith("Select", "URN"));
            }
        }
    }
}