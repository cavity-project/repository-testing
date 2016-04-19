namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryDeleteKey<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            Record1 = repository.Insert(Record1);

            if (!Record1.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            if (!repository.Delete(Record1.Key.Value))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectTrueWhenExistingRecord_ExceptionMessage.FormatWith("Delete", "deleted"));
            }

            if (repository.Exists(Record1.Key.Value))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectWhenDoesNotExist_ExceptionMessage.FormatWith("Exists", "false"));
            }
        }
    }
}