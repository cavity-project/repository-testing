namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryUpsertInsertRecord<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Upsert(Record1);
            if (!record.Key.HasValue)
            {
                throw new RepositoryTestException(Resources.Repository_Insert_ReturnsIncorrectKey_ExceptionMessage);
            }

            if (!record.Created.HasValue)
            {
                throw new RepositoryTestException(Resources.Repository_Insert_ReturnsWithoutCreated_ExceptionMessage);
            }

            if (!record.Modified.HasValue)
            {
                throw new RepositoryTestException(Resources.Repository_Insert_ReturnsWithoutModified_ExceptionMessage);
            }
        }
    }
}