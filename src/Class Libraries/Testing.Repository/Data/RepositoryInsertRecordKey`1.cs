namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryInsertRecordKey<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        public RepositoryInsertRecordKey()
        {
            Record1.Key = AlphaDecimal.Random();
        }

        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            RepositoryException expected = null;
            try
            {
                repository.Insert(Record1);
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
                throw new RepositoryTestException(Resources.Repository_Insert_RecordKeyExists_ExceptionMessage);
            }
        }
    }
}