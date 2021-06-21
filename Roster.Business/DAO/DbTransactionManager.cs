using Roster.Data.DBAccessor;

namespace Roster.Business.DAO
{
    public class DbTransactionManager : IDbTransactionManager
    {
        private readonly IDbContext context;

        public DbTransactionManager(IDbContext context)
        {            

            this.context = context;
        }

        public void BeginTransaction()
        {
            this.context.BeginTransaction();
        }

        public void Commit()
        {
            this.context.Commit();
        }

        public void Rollback()
        {
            this.context.Rollback();
        }
    }
}
