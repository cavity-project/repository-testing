namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryDeleteUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            if (!repository.Delete(Record1.Urn))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectTrueWhenExistingRecord_ExceptionMessage.FormatWith("Delete", "deleted"));
            }

            if (repository.Exists(Record1.Urn))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectWhenDoesNotExist_ExceptionMessage.FormatWith("Exists", "false"));
            }
        }
    }
}