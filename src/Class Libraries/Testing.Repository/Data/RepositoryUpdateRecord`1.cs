namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryUpdateRecord<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Insert(Record1);
            if (record.Key ==
                repository.Update(record).Key)
            {
                return;
            }

            throw new RepositoryTestException(Resources.Repository_ExpectTrueWhenExistingRecord_ExceptionMessage.FormatWith("Update", "updated"));
        }
    }
}