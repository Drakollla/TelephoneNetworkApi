namespace TelephoneNetworkApi.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
