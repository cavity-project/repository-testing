namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryToKeyUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var insert = repository.Insert(Record1).Key;
            var key = repository.ToKey(Record1.Urn);

            if (null == key)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectResult_ExceptionMessage.FormatWith("ToKey", "key"));
            }

            if (key != insert)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectCorrectValue_ExceptionMessage.FormatWith("ToKey", "key"));
            }
        }
    }
}