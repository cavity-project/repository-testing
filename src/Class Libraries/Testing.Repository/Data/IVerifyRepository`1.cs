namespace Cavity.Data
{
    public interface IVerifyRepository<T>
        where T : new()
    {
        void Verify(IRepository<T> repository);
    }
}