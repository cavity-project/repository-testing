namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryToUrnKey<T> : VerifyRepositoryBase<T>
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

            var urn = repository.ToUrn(Record1.Key.Value);

            if (null == urn)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectResult_ExceptionMessage.FormatWith("ToUrn", "URN"));
            }

            if (urn != Record1.Urn)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectCorrectValue_ExceptionMessage.FormatWith("ToUrn", "URN"));
            }
        }
    }
}