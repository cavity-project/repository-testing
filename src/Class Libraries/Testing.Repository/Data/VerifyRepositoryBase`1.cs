namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;

    public abstract class VerifyRepositoryBase<T> : IVerifyRepository<T>
        where T : new()
    {
        protected VerifyRepositoryBase()
        {
            Record1 = NewRecord();
            Record2 = NewRecord();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public IRecord<T> Record1 { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public IRecord<T> Record2 { get; set; }

        public void Verify(IRepository<T> repository)
        {
            using (new TransactionScope())
            {
                OnVerify(repository);
            }
        }

        protected abstract void OnVerify(IRepository<T> repository);

        private static Record<T> NewRecord()
        {
            return new Record<T>
                       {
                           Cacheability = "public",
                           Expiration = "P1D",
                           Status = 200,
                           Urn = "urn://example.com/" + Guid.NewGuid(),
                           Value = Activator.CreateInstance<T>()
                       };
        }
    }
}