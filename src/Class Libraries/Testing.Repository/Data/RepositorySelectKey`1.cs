namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositorySelectKey<T> : VerifyRepositoryBase<T>
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

            var record = repository.Select(Record1.Key.Value);

            if (null == record)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectResult_ExceptionMessage.FormatWith("Select", "record"));
            }

            if (Record1.Key !=
                record.Key)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectCorrectRecordValue_ExceptionMessage.FormatWith("Select", "key"));
            }

            if (!Record1.Urn.Equals(record.Urn))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectCorrectRecordValue_ExceptionMessage.FormatWith("Select", "URN"));
            }

            if (ReferenceEquals(Record1.Value, null))
            {
                if (ReferenceEquals(record.Value, null))
                {
                    return;
                }

                throw new RepositoryTestException(Resources.Repository_ExpectCorrectRecordValue_ExceptionMessage.FormatWith("Select", "value"));
            }

            if (!Record1.Value.Equals(record.Value))
            {
                throw new RepositoryTestException(Resources.Repository_ExpectCorrectRecordValue_ExceptionMessage.FormatWith("Select", "value"));
            }
        }
    }
}