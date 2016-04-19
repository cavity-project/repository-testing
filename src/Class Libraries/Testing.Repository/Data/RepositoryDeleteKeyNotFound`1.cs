namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryDeleteKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Delete(AlphaDecimal.Random()))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectWhenDoesNotExist_ExceptionMessage.FormatWith("Delete", "false"));
            }
        }
    }
}