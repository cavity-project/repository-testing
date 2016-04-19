namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryExistsKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Exists(AlphaDecimal.Random()))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectWhenDoesNotExist_ExceptionMessage.FormatWith("Exists", "false"));
            }
        }
    }
}