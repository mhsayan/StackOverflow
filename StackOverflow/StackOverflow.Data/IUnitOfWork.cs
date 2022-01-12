namespace StackOverflow.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
