using Roster.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
namespace Roster.Business.DAO
{
    public class ProductRepository : GenericRepository<TBL_ProductList>, IProductRepository
    {
        public ProductRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<TBL_ProductList> Filter(List<string> productCode)
        {
            return this.dbSet.Where(p => productCode.Contains(p.sProductCode));
        }
    }
}
