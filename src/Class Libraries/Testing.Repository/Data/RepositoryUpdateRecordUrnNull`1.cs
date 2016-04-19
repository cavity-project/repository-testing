namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryUpdateRecordUrnNull<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Insert(Record1);
            record.Urn = null;

            RepositoryException expected = null;
            try
            {
                repository.Update(record);
            }
            catch (RepositoryException exception)
            {
                expected = exception;
            }
            catch (Exception exception)
            {
                throw new RepositoryTestException(Resources.Repository_UnexpectedException_ExceptionMessage, exception);
            }

            if (null == expected)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectExceptionWhenRecordIncomplete_ExceptionMessage.FormatWith("Update", "URN"));
            }
        }
    }
}