namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryUpdateRecordKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            RepositoryException expected = null;
            try
            {
                Record2.Key = AlphaDecimal.Random();
                repository.Update(Record2);
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
                throw new RepositoryTestException(Resources.Repository_ExpectExceptionWhenRecordKeyNotFound_UnitTestExceptionMessage.FormatWith("Update"));
            }
        }
    }
}