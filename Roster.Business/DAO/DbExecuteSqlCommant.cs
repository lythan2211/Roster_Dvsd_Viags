using Roster.Data.DBAccessor;

namespace Roster.Business.DAO
{
    public class DbExecuteSqlCommant : IDbExecuteSqlCommant
    {
        private readonly IDbContext context;

        public DbExecuteSqlCommant(IDbContext context)
        {            
            this.context = context;
        }

        public int ExcecuteCommant(string sql, object[] parameters)
        {
            return this.context.ExecuteSqlCommmant(sql, parameters);
        }
      
    }
}
