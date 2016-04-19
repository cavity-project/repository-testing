namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryInsertRecordUrnExists<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            try
            {
                repository.Insert(Record1);
            }
            catch (Exception exception)
            {
                throw new RepositoryTestException(Resources.Repository_UnexpectedException_ExceptionMessage, exception);
            }

            RepositoryException expected = null;
            try
            {
                Record2.Urn = Record1.Urn;
                repository.Insert(Record2);
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
                throw new RepositoryTestException(Resources.Repository_Insert_RecordUrnExists_ExceptionMessage);
            }
        }
    }
}