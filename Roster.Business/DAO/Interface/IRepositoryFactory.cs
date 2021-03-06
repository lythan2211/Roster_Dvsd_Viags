namespace Roster.Business.DAO
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>(params object[] args) where T : class;
        IDbTransactionManager GetTransactionManager();
    }
}
